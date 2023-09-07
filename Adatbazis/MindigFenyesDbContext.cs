using Adatbazis.Models;
using Microsoft.EntityFrameworkCore;

namespace Adatbazis
{
    public class MindigFenyesDbContext : DbContext
    {
        public MindigFenyesDbContext()
            : base(new DbContextOptionsBuilder()
                  .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MindigFenyesDB;Trusted_Connection=True;")
                  .UseLazyLoadingProxies()
                  .Options)
        {
        }

        public DbSet<Dolgozo> Dolgozok { get; set; }

        public DbSet<JavitasTipus> JavitasTipusok { get; set; }

        public DbSet<Bejelentes> Bejelentesek { get; set; }
    }
}
