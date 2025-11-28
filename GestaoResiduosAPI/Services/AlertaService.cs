using GestaoResiduosAPI.Data;
using GestaoResiduosAPI.Models;
using GestaoResiduosAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestaoResiduosAPI.Services
{
    public class AlertaService
    {
        private readonly AppDbContext _db;

        public AlertaService(AppDbContext db)
        {
            _db = db;
        }

        public async Task GerarAlertaAsync(Alerta alerta)
        {
            _db.Alertas.Add(alerta);
            await _db.SaveChangesAsync();
        }

        public async Task<List<AlertaViewModel>> ListarAsync()
        {
            return await _db.Alertas
                .Include(a => a.PontoColeta)
                .OrderByDescending(a => a.DataHora)
                .Select(a => new AlertaViewModel
                {
                    Id = a.Id,
                    Ponto = a.PontoColeta.Nome,
                    Mensagem = a.Mensagem,
                    DataHora = a.DataHora
                })
                .ToListAsync();
        }
    }
}
