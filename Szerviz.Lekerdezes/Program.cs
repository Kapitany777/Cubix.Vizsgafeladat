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

            Console.WriteLine("A programból a -1 érték beírásával lehet kilépni.");
            Console.WriteLine();
        }

        static string SzuroFeltetelBekerese()
        {
            string? valasz = string.Empty;
            int feltetel;

            do
            {
                Console.Write("Kérem a szűrőfeltételt: ");
                valasz = Console.ReadLine();
            } while (!int.TryParse(valasz, out feltetel));

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
                Console.WriteLine($"\t{bejelentes.HibaLeiras}");
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            var dbContext = new MindigFenyesDbContext();
            var lekerdezesek = new BejelentesLekerdezesek(dbContext.Bejelentesek);

            FejlecKiirasa();

            string szuroFeltetel = string.Empty;

            do
            {
                try
                {
                    szuroFeltetel = SzuroFeltetelBekerese();

                    if (szuroFeltetel == "-1")
                    {
                        break;
                    }

                    IEnumerable<Bejelentes>? eredmeny = null;

                    if (szuroFeltetel.Length == 4)
                    {
                        eredmeny = lekerdezesek.BejelentesekIranyitoszamAlapjan(szuroFeltetel);
                    }
                    else if (szuroFeltetel.Length == 3)
                    {
                        eredmeny = lekerdezesek.BejelentesekIranyitoszamKezdetAlapjan(szuroFeltetel);
                    }
                    else if (szuroFeltetel.Length <= 2)
                    {
                        eredmeny = lekerdezesek.BejelentesekNapokAlapjan(DateTime.Now, int.Parse(szuroFeltetel));
                    }

                    if (eredmeny != null)
                    {
                        EredmenyKiirasa(eredmeny);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba történt: {ex.Message}");
                }
            } while (true);
        }
    }
}