using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Modelo;

namespace Contexto
{
    public class MonedaDb : DbContext
    {
 
        public MonedaDb(DbContextOptions<MonedaDb> options) : base(options)
        {
        }

        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<FactorConversion> FactoresConversion { get; set; }
        public DbSet<Operacion> Operacion { get; set; }
        public DbSet<Pais> Paises { get; set; }


        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<MonedaDb>
        {
            public MonedaDb CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<MonedaDb>();
                optionsBuilder.UseSqlServer(
                    "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MonedaDb; Integrated Security = True; MultipleActiveResultSets = True");

                return new MonedaDb(optionsBuilder.Options);
            }
        }


    }
}
