using Adatbazis.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adatbazis
{
    public class MindigFenyesDbContext : DbContext
    {
        public MindigFenyesDbContext()
            : base(new DbContextOptionsBuilder().UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=MindigFenyesDB;Trusted_Connection=True;"
            ).Options)
        {
        }

        public DbSet<Dolgozo> Dolgozok { get; set; }

        public DbSet<Javitas> Javitasok { get; set; }

        public DbSet<Bejelentes> Bejelentesek { get; set; }
    }
}
