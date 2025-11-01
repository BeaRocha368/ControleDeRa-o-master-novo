using ControleDeRação.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeRação.Data
{
    public class BancoContexto : DbContext
    {
        public BancoContexto(DbContextOptions<BancoContexto> options) : base(options)
        {

        }
        //public override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new ());
        //}

        public DbSet<Pet> Pets { get; set; }
    }
}
