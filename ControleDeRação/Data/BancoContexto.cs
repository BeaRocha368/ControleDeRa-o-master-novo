using ControleDeRação.Models;
using Microsoft.EntityFrameworkCore;
using ControleDeRação.Models;
using ControleDeRação.Data.Mapeamento;

namespace ControleDeRação.Data
{
    public class BancoContexto : DbContext
    {
        public BancoContexto(DbContextOptions<BancoContexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PetMapeamento());
            modelBuilder.ApplyConfiguration(new RacaoMapeamento());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Racao> Racoes { get; set; }

    }
}

