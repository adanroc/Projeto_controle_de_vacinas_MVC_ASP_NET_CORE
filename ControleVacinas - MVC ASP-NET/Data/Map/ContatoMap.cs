using ControleVacinas___MVC_ASP_NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleVacinas___MVC_ASP_NET.Data.Map
{
    public class ContatoMap : IEntityTypeConfiguration<ListaDeCadastrosModel>
    {
        public void Configure(EntityTypeBuilder<ListaDeCadastrosModel> builder)
        {
            builder.HasKey(x => x.Id); //Chave Primaria            
            builder.HasOne(x => x.Usuario);

        }
    }
}
