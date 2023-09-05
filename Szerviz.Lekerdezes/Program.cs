using Adatbazis;
using Adatbazis.Models;
using Lekerdezesek;

namespace Szerviz.Lekerdezes
{
    internal class Program
    {
        static void FejlecKiirasa()
        {
            Console.WriteLine("Szerviz lekérdező Console1 modul");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();

            Console.WriteLine("A programból a 0 beírásával lehet kilépni.");
            Console.WriteLine();
        }

        static string SzuroFeltetelBekerese()
        {
            string szoveg = string.Empty;
            int feltetel;

            do
            {
                Console.Write("Kérem a szűrőfeltételt: ");
                szoveg = Console.ReadLine();
            } while (!int.TryParse(szoveg, out feltetel));

            return feltetel.ToString();
        }

        static void EredmenyKiirasa(IEnumerable<Bejelentes> bejelentesek)
        {
            if (bejelentesek.Count() == 0)
            {
                Console.WriteLine("Üres eredményhalmaz!");
                return;
            }
            
            foreach (var bejelentes in bejelentesek)
            {
                Console.WriteLine($"{bejelentes.Id} - {bejelentes.Iranyitoszam} {bejelentes.Varos}, {bejelentes.Cim}");
            }
        }

        static void Main(string[] args)
        {
            var dbContext = new MindigFenyesDbContext();
            var database = new LekerdezesAdatbazis(dbContext.Dolgozok, dbContext.JavitasTipusok, dbContext.Bejelentesek);
            var lekerdezesek = new MindigFenyesLekerdezesek(database);

            FejlecKiirasa();

            string szuroFeltetel = string.Empty;

            do
            {
                try
                {
                    szuroFeltetel = SzuroFeltetelBekerese();

                    if (szuroFeltetel.Length == 4)
                    {
                        var eredmeny = lekerdezesek.BejelentesekIranyitoszamAlapjan(szuroFeltetel);
                        EredmenyKiirasa(eredmeny);
                    }
                    else if (szuroFeltetel.Length == 3)
                    {
                        var eredmeny = lekerdezesek.BejelentesekIranyitoszamKezdetAlapjan(szuroFeltetel);
                        EredmenyKiirasa(eredmeny);
                    }
                    else if (szuroFeltetel.Length <= 2)
                    {

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba történt: {ex.Message}");
                }
            } while (szuroFeltetel != "0");
        }
    }
}