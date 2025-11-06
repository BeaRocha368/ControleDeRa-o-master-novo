using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ControleDeRação.Models;

namespace ControleDeRação.Data.Mapeamento
{
    public class RacaoMapeamento : IEntityTypeConfiguration<Racao>
    {
        public void Configure(EntityTypeBuilder<Racao> builder)
        {
            builder.ToTable("Racoes");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.ConsumoDiarioKg)
                   .HasColumnType("DECIMAL(8, 2)") // Precisão para peso
                   .IsRequired();

            builder.Property(t => t.EstoqueAtualKg)
                   .HasColumnType("DECIMAL(8, 2)")
                   .IsRequired();

            builder.Property(t => t.UltimaCompraKg)
                   .HasColumnType("DECIMAL(8, 2)")
                   .IsRequired();

            builder.Property(t => t.DataAtualizacao)
                   .HasColumnType("DATETIME2")
                   .IsRequired();

            
        }
    }
}
