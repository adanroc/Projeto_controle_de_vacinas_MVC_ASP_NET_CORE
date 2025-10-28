using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleVacinas___MVC_ASP_NET.Migrations
{
    /// <inheritdoc />
    public partial class Zero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListaDeUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Perfil = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaDeUsuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListaDeCadastros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumMatricula = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Setor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SituacaoVacinacao = table.Column<int>(type: "int", nullable: true),
                    DataDose1HepatiteB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDose2HepatiteB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDose3HepatiteB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDoseReforcoHepatiteB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExameAntiHBS = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResultadoExameAntiHBS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataDose1DifteriaTetano = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDose2DifteriaTetano = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDose3DifteriaTetano = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDoseReforcoDifteriaTetano = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDose1TripliceViral = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDose2TripliceViral = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDose1Covid = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDose2Covid = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDose3Covid = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDoseReforco1Covid = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDoseReforco2Covid = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDoseUnicaFebreAmarela = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDoseAnualInfluenza = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApagadoUsuarioPadrao = table.Column<int>(type: "int", nullable: true),
                    DataApagado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    TotalTiposDeVacinas = table.Column<int>(type: "int", nullable: true),
                    TotalTiposDeVacinasAplicadas = table.Column<int>(type: "int", nullable: true),
                    TotalTiposDeVacinasFaltando = table.Column<int>(type: "int", nullable: true),
                    TotalDoses = table.Column<int>(type: "int", nullable: true),
                    TotalDosesAplicadas = table.Column<int>(type: "int", nullable: true),
                    TotalDosesFaltando = table.Column<int>(type: "int", nullable: true),
                    TotalDosesHepatiteB = table.Column<int>(type: "int", nullable: true),
                    TotalDosesAplicadasHepatiteB = table.Column<int>(type: "int", nullable: true),
                    TotalDosesFaltandoHepatiteB = table.Column<int>(type: "int", nullable: true),
                    TotalDosesExameAntiHBS = table.Column<int>(type: "int", nullable: true),
                    TotalDosesAplicadasExameAntiHBS = table.Column<int>(type: "int", nullable: true),
                    TotalDosesFaltandoExameAntiHBS = table.Column<int>(type: "int", nullable: true),
                    TotalDosesDifteriaTetano = table.Column<int>(type: "int", nullable: true),
                    TotalDosesAplicadasDifteriaTetano = table.Column<int>(type: "int", nullable: true),
                    TotalDosesFaltandoDifteriaTetano = table.Column<int>(type: "int", nullable: true),
                    TotalDosesTripliceViral = table.Column<int>(type: "int", nullable: true),
                    TotalDosesAplicadasTripliceViral = table.Column<int>(type: "int", nullable: true),
                    TotalDosesFaltandoTripliceViral = table.Column<int>(type: "int", nullable: true),
                    TotalDosesCovid = table.Column<int>(type: "int", nullable: true),
                    TotalDosesAplicadasCovid = table.Column<int>(type: "int", nullable: true),
                    TotalDosesFaltandoCovid = table.Column<int>(type: "int", nullable: true),
                    TotalDosesFebreAmarela = table.Column<int>(type: "int", nullable: true),
                    TotalDosesAplicadasFebreAmarela = table.Column<int>(type: "int", nullable: true),
                    TotalDosesFaltandoFebreAmarela = table.Column<int>(type: "int", nullable: true),
                    TotalDosesInfluenza = table.Column<int>(type: "int", nullable: true),
                    TotalDosesAplicadasInfluenza = table.Column<int>(type: "int", nullable: true),
                    TotalDosesFaltandoInfluenza = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaDeCadastros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListaDeCadastros_ListaDeUsuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "ListaDeUsuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListaDeCadastros_UsuarioId",
                table: "ListaDeCadastros",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListaDeCadastros");

            migrationBuilder.DropTable(
                name: "ListaDeUsuarios");
        }
    }
}
