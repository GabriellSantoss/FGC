using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JogoService.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirRelacionamentosJogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Categorias_CategoriaId",
                table: "Jogos");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_EmpresasFornecedoras_EmpresaFornecedoraId",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_CategoriaId",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_EmpresaFornecedoraId",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "EmpresaFornecedoraId",
                table: "Jogos");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tamanho",
                table: "Jogos",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_IdCategoria",
                table: "Jogos",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_IdFornecedor",
                table: "Jogos",
                column: "IdFornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Categorias_IdCategoria",
                table: "Jogos",
                column: "IdCategoria",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_EmpresasFornecedoras_IdFornecedor",
                table: "Jogos",
                column: "IdFornecedor",
                principalTable: "EmpresasFornecedoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Categorias_IdCategoria",
                table: "Jogos");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_EmpresasFornecedoras_IdFornecedor",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_IdCategoria",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_IdFornecedor",
                table: "Jogos");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tamanho",
                table: "Jogos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddColumn<long>(
                name: "CategoriaId",
                table: "Jogos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmpresaFornecedoraId",
                table: "Jogos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_CategoriaId",
                table: "Jogos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_EmpresaFornecedoraId",
                table: "Jogos",
                column: "EmpresaFornecedoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Categorias_CategoriaId",
                table: "Jogos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_EmpresasFornecedoras_EmpresaFornecedoraId",
                table: "Jogos",
                column: "EmpresaFornecedoraId",
                principalTable: "EmpresasFornecedoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
