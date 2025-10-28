 using ControleVacinas___MVC_ASP_NET.Controllers;
using ControleVacinas___MVC_ASP_NET.Data.Map;
using ControleVacinas___MVC_ASP_NET.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleVacinas___MVC_ASP_NET.Data
{
    public class BancoContext : DbContext
    {
        // Recursos do Entity-Framework-Core

        // Criar e configurar construtor da classe
        // Injetar como parâmetro de entrada: DBContextOptions
        // Tipar o parâmetro como BancoContext (a própria classe)
        // Dar um nome de options para a injeção (para esse parâmetro DBContextOptions)
        // Passar a informação que vai vim do options para dentro do construtor padrão (DBcontext), simplemente chamando o options
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        // Configurar a entidade ListaDeCadastrosModel dentro do contexto
        // Esse contexto BancoContext pode ter várias tabelas, então eu tenho um banco com várias tabelas

        // Primeiro contexto de tabela DbSet
        // Informar a classe que representa a tabela do banco de dados
        // Importa-la
        // Dar o nome de ListaDeCadastros
        public DbSet<ListaDeCadastrosModel> ListaDeCadastros { get; set; }

        public DbSet<ListaDeUsuariosModel> ListaDeUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
