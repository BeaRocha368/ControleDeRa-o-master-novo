using ControleDeRacao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeRacao.Data.Mapeamento
{
    public class AgendaAlimentacaoMapeamento : IEntityTypeConfiguration<AgendaAlimentacao>
    {
        public void Configure(EntityTypeBuilder<AgendaAlimentacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Turno)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.QuantidadeKg)
                .IsRequired();

            builder.HasOne(x => x.Pet)
                .WithMany()
                .HasForeignKey(x => x.PetCodigo)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("AgendaAlimentacao");
        }
    }
}
