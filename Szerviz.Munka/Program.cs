using Adatbazis;
using Adatbazis.Models;
using Lekerdezesek;

namespace Szerviz.Munka
{
    internal class Program
    {
        private static void FejlecKiirasa()
        {
            Console.WriteLine("Szerviz feladat lezáró Console2 modul");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();
        }

        private static void BejelentesekKiirasa(IEnumerable<Bejelentes> bejelentesek)
        {
            Console.WriteLine("A nyitott bejelentések:");
            Console.WriteLine();

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

        private static void DolgozokKiirasa(IEnumerable<Dolgozo> dolgozok)
        {
            Console.WriteLine("A dolgozók:");
            Console.WriteLine();

            if (dolgozok.Count() == 0)
            {
                Console.WriteLine("Üres eredményhalmaz!");
                return;
            }

            foreach (var dolgozo in dolgozok)
            {
                Console.WriteLine($"{dolgozo.Id} - {dolgozo.VezetekNev} {dolgozo.KeresztNev}");
            }

            Console.WriteLine();
        }

        private static int BejelentesBekerese(IEnumerable<Bejelentes> bejelentesek)
        {
            int id = 0;

            do
            {
                Console.Write("Melyik bejelentést szeretnéd lezárni? ");
                string? valasz = Console.ReadLine();

                if (!int.TryParse(valasz, out id))
                {
                    Console.WriteLine("Hibás szám!");
                }
                else if (bejelentesek.Count(b => b.Id == id) == 0)
                {
                    Console.WriteLine("Hibás bejelentés azonosító!");
                    id = 0;
                }

            } while (id == 0);

            return id;
        }

        private static int DolgozoBekerese(IEnumerable<Dolgozo> dolgozok)
        {
            int id = 0;

            do
            {
                Console.Write("Kérem a javítást végző dolgozó azonosítóját: ");
                string? valasz = Console.ReadLine();

                if (!int.TryParse(valasz, out id))
                {
                    Console.WriteLine("Hibás szám!");
                }
                else if (dolgozok.Count(b => b.Id == id) == 0)
                {
                    Console.WriteLine("Hibás dolgozó azonosító!");
                    id = 0;
                }

            } while (id == 0);

            return id;
        }

        private static void JavitasTipusokKiirasa(IEnumerable<JavitasTipus> javitasTipusok)
        {
            Console.WriteLine("A javítások:");
            Console.WriteLine();

            if (javitasTipusok.Count() == 0)
            {
                Console.WriteLine("Üres eredményhalmaz!");
                return;
            }

            foreach (var javitasTipus in javitasTipusok)
            {
                Console.WriteLine($"{javitasTipus.Id} - {javitasTipus.TipusNev}");
            }

            Console.WriteLine();
        }

        private static int JavitasTipusBekerese(IEnumerable<JavitasTipus> javitasTipusok)
        {
            int id = 0;

            do
            {
                Console.Write("Kérem a javítás azonosítóját: ");
                string? valasz = Console.ReadLine();

                if (!int.TryParse(valasz, out id))
                {
                    Console.WriteLine("Hibás szám!");
                }
                else if (javitasTipusok.Count(b => b.Id == id) == 0)
                {
                    Console.WriteLine("Hibás javítás azonosító!");
                    id = 0;
                }

            } while (id == 0);

            return id;
        }

        private static void BejelentesLezarasa()
        {
            var dbContext = new MindigFenyesDbContext();
            var bejelentesLekerdezesek = new BejelentesLekerdezesek(dbContext.Bejelentesek);
            var dolgozoLekerdezesek = new DolgozoLekerdezesek(dbContext.Dolgozok);
            var javitasTipusLekerdezesek = new JavitasTipusLekerdezesek(dbContext.JavitasTipusok);

            FejlecKiirasa();

            var bejelentesek = bejelentesLekerdezesek.NyitottBejelentesek();
            BejelentesekKiirasa(bejelentesek);
            int bejelentesId = BejelentesBekerese(bejelentesek);
            Console.WriteLine();

            var dolgozok = dolgozoLekerdezesek.DolgozokListaja();
            DolgozokKiirasa(dolgozok);
            int dolgozoId = DolgozoBekerese(dolgozok);
            Console.WriteLine();

            var javitasTipusok = javitasTipusLekerdezesek.JavitasTipusokListaja();
            JavitasTipusokKiirasa(javitasTipusok);
            int javitasTipusId = JavitasTipusBekerese(javitasTipusok);
            Console.WriteLine();

            var bejelentes = dbContext.Bejelentesek.Find(bejelentesId);

            if (bejelentes != null)
            {
                bejelentes.DolgozoId = dolgozoId;
                bejelentes.JavitasTipusId = javitasTipusId;
                bejelentes.JavitasDatuma = DateTime.Today;

                dbContext.SaveChanges();
            }

            Console.WriteLine("A bejelentés lezárása elkészült!");
        }

        static void Main(string[] args)
        {
            try
            {
                BejelentesLezarasa();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt!{Environment.NewLine}{ex.Message}");
            }
        }
    }
}