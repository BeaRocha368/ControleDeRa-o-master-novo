using ControleDeRacao.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ControleDeRacao.Data.Mapeamento
{
    public class AgendaMapeamento : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.ToTable("Agenda");

            builder.HasKey(t => t.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.HorarioMatutino).HasColumnType("VARCHAR(20)");
            builder.Property(t => t.HorarioVespertino).HasColumnType("VARCHAR(20)");
            builder.Property(t => t.HorarioNoturno).HasColumnType("VARCHAR(20)");
         
        }
    }
}
