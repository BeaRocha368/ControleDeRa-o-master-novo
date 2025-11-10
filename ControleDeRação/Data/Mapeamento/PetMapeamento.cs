using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ControleDeRacao.Models;

namespace ControleDeRacao.Data.Mapeamento
{
    public class PetMapeamento : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("Pets");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome).HasColumnType("VARCHAR(100)");
            builder.Property(t => t.Idade).HasColumnType("INT");
            builder.Property(t => t.Peso).HasColumnType("FLOAT");
            builder.Property(t => t.MarcaRacao).HasColumnType("VARCHAR(100)");
            builder.Property(t => t.CodigoAcesso).HasColumnType("VARCHAR(100)");
            builder.Property(t => t.DataCriacao).HasColumnType("DATATIME2");
           
        }
    }
}
