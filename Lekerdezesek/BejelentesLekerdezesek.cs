using Adatbazis.Models;

namespace Lekerdezesek
{
    public class BejelentesLekerdezesek
    {
        public IEnumerable<Bejelentes> Bejelentesek { get; }

        public BejelentesLekerdezesek(IEnumerable<Bejelentes> bejelentesek)
        {
            Bejelentesek = bejelentesek;
        }

        /// <summary>
        /// A nyitott bejelentések lekérdezése
        /// </summary>
        /// <returns>A bejelentések listája</returns>
        public IEnumerable<Bejelentes> NyitottBejelentesek()
        {
            return Bejelentesek
                .Where(b => b.JavitasTipusId == null)
                .OrderBy(b => b.Iranyitoszam)
                .ThenBy(b => b.Id);
        }

        /// <summary>
        /// A bejelentések lekérdezése egy adott irányítószám alapján
        /// </summary>
        /// <param name="iranyitoszam">A keresett irányítószám</param>
        /// <returns>A bejelentések listája</returns>
        public IEnumerable<Bejelentes> BejelentesekIranyitoszamAlapjan(string iranyitoszam)
        {
            return Bejelentesek
                .Where(b => b.Iranyitoszam == iranyitoszam && b.JavitasTipusId == null)
                .OrderBy(b => b.Iranyitoszam)
                .ThenBy(b => b.Id);
        }

        /// <summary>
        /// A bejelentések lekérdezése egy adott irányítószám kezdet alapján
        /// </summary>
        /// <param name="iranyitoszamKezdet">A keresett 3 karakteres irányítószám kezdet</param>
        /// <returns>A bejelentések listája</returns>
        public IEnumerable<Bejelentes> BejelentesekIranyitoszamKezdetAlapjan(string iranyitoszamKezdet)
        {
            return Bejelentesek
                .Where(b => b.Iranyitoszam.StartsWith(iranyitoszamKezdet) && b.JavitasTipusId == null)
                .OrderBy(b => b.Iranyitoszam)
                .ThenBy(b => b.Id);
        }

        /// <summary>
        /// Egy adott napnál régebbi bejelentések lekérdezése
        /// </summary>
        /// <param name="aktualisNap">Az aktuális nap, amihez hasonlítani kell</param>
        /// <param name="napokSzama">A napok száma</param>
        /// <returns>A bejelentések listája</returns>
        public IEnumerable<Bejelentes> BejelentesekNapokAlapjan(DateTime aktualisNap, int napokSzama)
        {
            return Bejelentesek
                .Where(b => (aktualisNap - b.BejelentesDatuma).TotalDays >= napokSzama && b.JavitasTipusId == null)
                .OrderBy(b => b.BejelentesDatuma)
                .ThenBy(b => b.Iranyitoszam)
                .ThenBy(b => b.Id);
        }
    }
}