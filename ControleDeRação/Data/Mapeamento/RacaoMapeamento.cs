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

            builder.HasKey(r => r.Id);

            builder.Property(r => r.ConsumoDiarioKg)
                   .HasColumnType("DECIMAL(8, 2)") // Precisão para peso
                   .IsRequired();

            builder.Property(r => r.EstoqueAtualKg)
                   .HasColumnType("DECIMAL(8, 2)")
                   .IsRequired();

            builder.Property(r => r.UltimaCompraKg)
                   .HasColumnType("DECIMAL(8, 2)")
                   .IsRequired();

            builder.Property(r => r.DataAtualizacao)
                   .HasColumnType("DATETIME2")
                   .IsRequired();

            // Configuração da Chave Estrangeira 
            builder.HasOne(r => r.Pet) // Racao tem um Pet
                   .WithMany() // Pet pode ter muitas Racoes (historico) 
                   .HasForeignKey(r => r.PetId) // Usando a chave estrangeira PetId
                   .IsRequired();
        }
    }
}
