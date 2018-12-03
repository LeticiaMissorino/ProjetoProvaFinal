using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoFinal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    telefone = table.Column<string>(nullable: true),
                    endereco = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    telefone = table.Column<string>(nullable: true),
                    endereco = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Embalagem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    tamanho = table.Column<decimal>(nullable: false),
                    produtoID = table.Column<int>(nullable: true),
                    fornecedorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Embalagem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Embalagem_Fornecedor_fornecedorID",
                        column: x => x.fornecedorID,
                        principalTable: "Fornecedor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Embalagem_Produto_produtoID",
                        column: x => x.produtoID,
                        principalTable: "Produto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ordem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    clienteID = table.Column<int>(nullable: true),
                    data = table.Column<DateTime>(nullable: false),
                    hora = table.Column<TimeSpan>(nullable: false),
                    produtoID = table.Column<int>(nullable: true),
                    qdte = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ordem_Cliente_clienteID",
                        column: x => x.clienteID,
                        principalTable: "Cliente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ordem_Produto_produtoID",
                        column: x => x.produtoID,
                        principalTable: "Produto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnaliseQualidade",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data = table.Column<DateTime>(nullable: false),
                    aprovado = table.Column<bool>(nullable: false),
                    responsavel = table.Column<string>(nullable: true),
                    ordemID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnaliseQualidade", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AnaliseQualidade_Ordem_ordemID",
                        column: x => x.ordemID,
                        principalTable: "Ordem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnaliseQualidade_ordemID",
                table: "AnaliseQualidade",
                column: "ordemID");

            migrationBuilder.CreateIndex(
                name: "IX_Embalagem_fornecedorID",
                table: "Embalagem",
                column: "fornecedorID");

            migrationBuilder.CreateIndex(
                name: "IX_Embalagem_produtoID",
                table: "Embalagem",
                column: "produtoID");

            migrationBuilder.CreateIndex(
                name: "IX_Ordem_clienteID",
                table: "Ordem",
                column: "clienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Ordem_produtoID",
                table: "Ordem",
                column: "produtoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnaliseQualidade");

            migrationBuilder.DropTable(
                name: "Embalagem");

            migrationBuilder.DropTable(
                name: "Ordem");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Produto");
        }
    }
}
