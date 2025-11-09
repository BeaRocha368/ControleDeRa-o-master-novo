
using Microsoft.EntityFrameworkCore;
using ControleDeRacao.Models;
using ControleDeRacao.Data.Mapeamento;

namespace ControleDeRacao.Data
{
    public class BancoContexto : DbContext
    {
        public BancoContexto(DbContextOptions<BancoContexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PetMapeamento());
            modelBuilder.ApplyConfiguration(new RacaoMapeamento());
            modelBuilder.ApplyConfiguration(new AgendaAlimentacaoMapeamento());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Racao> Racoes { get; set; }
        public DbSet<AgendaAlimentacao> AgendaAlimentacoes { get; set; }

        
    }
}

