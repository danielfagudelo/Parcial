using Microsoft.EntityFrameworkCore;
using Parcial.Shared.Entities;

namespace Parcial.API.Data
{
    public class DataContext : DbContext
    {
       
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Boleta>Boletas{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Boleta>().HasIndex(x => x.Id).IsUnique();           
            

        }
    }
}

