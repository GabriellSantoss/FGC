using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JogoService.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirRelacionamentoAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Jogos_JogoId",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_JogoId",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "JogoId",
                table: "Avaliacoes");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_IdJogo",
                table: "Avaliacoes",
                column: "IdJogo");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Jogos_IdJogo",
                table: "Avaliacoes",
                column: "IdJogo",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Jogos_IdJogo",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_IdJogo",
                table: "Avaliacoes");

            migrationBuilder.AddColumn<long>(
                name: "JogoId",
                table: "Avaliacoes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_JogoId",
                table: "Avaliacoes",
                column: "JogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Jogos_JogoId",
                table: "Avaliacoes",
                column: "JogoId",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
