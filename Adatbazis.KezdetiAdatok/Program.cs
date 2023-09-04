using Adatbazis.Models;

namespace Adatbazis.KezdetiAdatok
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dolgozok = new List<Dolgozo>
            {
                new Dolgozo
                {
                    VezetekNev = "Kovács",
                    KeresztNev = "János"
                },

                new Dolgozo
                {
                    VezetekNev = "Szabó",
                    KeresztNev = "István"
                },

                new Dolgozo
                {
                    VezetekNev = "Nagy",
                    KeresztNev = "Béla"
                },

                new Dolgozo
                {
                    VezetekNev = "Kis",
                    KeresztNev = "Ferenc"
                }
            };

            var javitasok = new List<Javitas>
            {
                new Javitas { JavitasTipus = "Izzócsere" },
                new Javitas { JavitasTipus = "Lámpabura" },
                new Javitas { JavitasTipus = "Vezeték javítása" }
            };
            
            var context = new MindigFenyesDbContext();

            context.Dolgozok.AddRange(dolgozok);
            context.Javitasok.AddRange(javitasok);

            context.SaveChanges();
        }
    }
}