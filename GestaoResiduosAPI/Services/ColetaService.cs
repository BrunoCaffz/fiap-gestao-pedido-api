using GestaoResiduosAPI.Data;
using GestaoResiduosAPI.Models;
using GestaoResiduosAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestaoResiduosAPI.Services
{
    public class ColetaService
    {
        private readonly AppDbContext _db;
        private readonly AlertaService _alertaService;

        public ColetaService(AppDbContext db, AlertaService alertaService)
        {
            _db = db;
            _alertaService = alertaService;
        }

        public async Task<Coleta> RegistrarColetaAsync(ColetaCreateViewModel model)
        {
            var ponto = await _db.PontosColeta.FirstOrDefaultAsync(p => p.Id == model.PontoColetaId);
            if (ponto == null)
                throw new Exception("Ponto de coleta não encontrado.");

            // Verifica limite e gera alerta se necessário
            if (model.PesoKg > ponto.LimiteKg)
            {
                await _alertaService.GerarAlertaAsync(new Alerta
                {
                    PontoColetaId = ponto.Id,
                    Mensagem = $"Limite de {ponto.LimiteKg} kg excedido! Peso coletado: {model.PesoKg} kg."
                });
            }

            var coleta = new Coleta
            {
                ResiduoId = model.ResiduoId,
                PontoColetaId = model.PontoColetaId,
                VeiculoId = model.VeiculoId,
                ColetorId = model.ColetorId,
                PesoKg = model.PesoKg,
                DataHora = DateTime.UtcNow
            };

            _db.Coletas.Add(coleta);
            await _db.SaveChangesAsync();

            return coleta;
        }

        // Listagem com paginação
        public async Task<(List<ColetaListViewModel>, int totalItems)> ListarAsync(int page, int pageSize)
        {
            var query = _db.Coletas
                .AsNoTracking()
                .Include(c => c.Residuo)
                .Include(c => c.PontoColeta)
                .Include(c => c.Veiculo)
                .Include(c => c.Coletor)
                .OrderByDescending(c => c.DataHora);

            var totalItems = await query.CountAsync();

            var dados = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ColetaListViewModel
                {
                    Id = c.Id,
                    Residuo = c.Residuo.Tipo,
                    Ponto = c.PontoColeta.Nome,
                    Veiculo = c.Veiculo.Placa,
                    Coletor = c.Coletor.Nome,
                    PesoKg = c.PesoKg,
                    DataHora = c.DataHora
                })
                .ToListAsync();

            return (dados, totalItems);
        }

        public async Task<PagedResult<ColetaListViewModel>> ListarPaginadoAsync(int page, int pageSize)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _db.Coletas
                .AsNoTracking()
                .Include(c => c.Residuo)
                .Include(c => c.PontoColeta)
                .Include(c => c.Veiculo)
                .Include(c => c.Coletor)
                .OrderByDescending(c => c.DataHora);

            var totalItems = await query.CountAsync();

            var dados = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ColetaListViewModel
                {
                    Id = c.Id,
                    Residuo = c.Residuo.Tipo,
                    Ponto = c.PontoColeta.Nome,
                    Veiculo = c.Veiculo.Placa,
                    Coletor = c.Coletor.Nome,
                    PesoKg = c.PesoKg,
                    DataHora = c.DataHora
                })
                .ToListAsync();

            return new PagedResult<ColetaListViewModel>
            {
                Items = dados,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
    

    public async Task<ColetaDetalheViewModel?> ObterPorIdAsync(int id)
        {
            var coleta = await _db.Coletas
                .AsNoTracking()
                .Include(c => c.Residuo)
                .Include(c => c.PontoColeta)
                .Include(c => c.Veiculo)
                .Include(c => c.Coletor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coleta == null)
                return null;

            return new ColetaDetalheViewModel
            {
                Id = coleta.Id,
                DataHora = coleta.DataHora,
                PesoKg = coleta.PesoKg,
                Residuo = coleta.Residuo.Tipo,
                PontoColeta = coleta.PontoColeta.Nome,
                LimiteKgPonto = coleta.PontoColeta.LimiteKg,
                Veiculo = coleta.Veiculo.Placa,
                Coletor = coleta.Coletor.Nome,
                ExcedeuLimite = coleta.PesoKg > coleta.PontoColeta.LimiteKg
            };
        }
    }
}