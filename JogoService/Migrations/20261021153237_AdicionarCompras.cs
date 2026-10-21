using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JogoService.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCompras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PagamentoId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogoId = table.Column<long>(type: "bigint", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_PagamentoId",
                table: "Compras",
                column: "PagamentoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compras");
        }
    }
}
