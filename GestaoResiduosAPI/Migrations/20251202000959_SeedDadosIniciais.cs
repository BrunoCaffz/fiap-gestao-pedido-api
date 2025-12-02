using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoResiduosAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedDadosIniciais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coletores",
                columns: new[] { "Id", "Ativo", "Nome" },
                values: new object[] { 1, true, "João da Silva" });

            migrationBuilder.InsertData(
                table: "PontosColeta",
                columns: new[] { "Id", "Latitude", "LimiteKg", "Longitude", "Nome" },
                values: new object[] { 1, 0.0, 100.0, 0.0, "Ponto Central" });

            migrationBuilder.InsertData(
                table: "Residuos",
                columns: new[] { "Id", "Descricao", "Tipo" },
                values: new object[] { 1, "Resíduo plástico em geral", "Plástico" });

            migrationBuilder.InsertData(
                table: "Veiculos",
                columns: new[] { "Id", "Ativo", "Modelo", "Placa" },
                values: new object[] { 1, true, "Caminhão 3/4", "ABC-1234" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coletores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PontosColeta",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Residuos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Veiculos",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
