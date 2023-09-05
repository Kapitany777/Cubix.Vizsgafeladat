using Adatbazis.Models;

namespace Adatbazis.KezdetiAdatok
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dolgozo1 =
                new Dolgozo
                {
                    VezetekNev = "Kovács",
                    KeresztNev = "János"
                };

            var dolgozo2 =
                new Dolgozo
                {
                    VezetekNev = "Szabó",
                    KeresztNev = "István"
                };

            var dolgozo3 =
                new Dolgozo
                {
                    VezetekNev = "Nagy",
                    KeresztNev = "Béla"
                };

            var dolgozo4 =
                new Dolgozo
                {
                    VezetekNev = "Kis",
                    KeresztNev = "Ferenc"
                };

            var dolgozok = new List<Dolgozo>
            {
                dolgozo1,
                dolgozo2,
                dolgozo3,
                dolgozo4
            };

            var javitasTipus1 = new JavitasTipus { TipusNev = "Izzócsere" };
            var javitasTipus2 = new JavitasTipus { TipusNev = "Lámpabura" };
            var javitasTipus3 = new JavitasTipus { TipusNev = "Vezeték javítása" };

            var javitasTipusok = new List<JavitasTipus>
            {
                javitasTipus1,
                javitasTipus2,
                javitasTipus3
            };

            var bejelentesek = new List<Bejelentes>
            {
                new Bejelentes
                {
                    Iranyitoszam = "6600",
                    Varos = "Szentes",
                    Cim = "Kossuth u. 40.",
                    BejelentesDatuma = new DateTime(2023, 08, 01)
                },

                new Bejelentes
                {
                    Iranyitoszam = "6600",
                    Varos = "Szentes",
                    Cim = "Nagy Ferenc u. 4.",
                    BejelentesDatuma = new DateTime(2023, 08, 05)
                },

                new Bejelentes
                {
                    Iranyitoszam = "6640",
                    Varos = "Csongrád",
                    Cim = "Petőfi u. 25.",
                    BejelentesDatuma = new DateTime(2023, 08, 02)
                },

                new Bejelentes
                {
                    Iranyitoszam = "6635",
                    Varos = "Szegvár",
                    Cim = "Petőfi u. 15.",
                    BejelentesDatuma = new DateTime(2023, 08, 12)
                },

                new Bejelentes
                {
                    Iranyitoszam = "6630",
                    Varos = "Mindszent",
                    Cim = "Rákóczi u. 10.",
                    BejelentesDatuma = new DateTime(2023, 08, 12),
                    JavitasDatuma = new DateTime(2023, 08, 14),
                    Dolgozo = dolgozo1,
                    JavitasTipus = javitasTipus1
                }
            };

            var context = new MindigFenyesDbContext();

            context.Dolgozok.AddRange(dolgozok);
            context.JavitasTipusok.AddRange(javitasTipusok);
            context.Bejelentesek.AddRange(bejelentesek);

            context.SaveChanges();
        }
    }
}