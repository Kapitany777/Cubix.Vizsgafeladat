using Adatbazis.Models;

namespace Lekerdezesek
{
    public class MindigFenyesLekerdezesek
    {
        private LekerdezesAdatbazis lekerdezesAdatbazis;

        public MindigFenyesLekerdezesek(LekerdezesAdatbazis lekerdezesAdatbazis)
        {
            this.lekerdezesAdatbazis = lekerdezesAdatbazis;
        }

        public IEnumerable<Bejelentes> BejelentesekIranyitoszamAlapjan(string iranyitoszam)
        {
            return lekerdezesAdatbazis
                .Bejelentesek
                .Where(x => x.Iranyitoszam == iranyitoszam && x.JavitasTipusId == null)
                .OrderBy(x => x.Iranyitoszam)
                .ThenBy(x => x.Id);
        }

        public IEnumerable<Bejelentes> BejelentesekIranyitoszamKezdetAlapjan(string iranyitoszamKezdet)
        {
            return lekerdezesAdatbazis
                .Bejelentesek
                .Where(x => x.Iranyitoszam.StartsWith(iranyitoszamKezdet) && x.JavitasTipusId == null)
                .OrderBy(x => x.Iranyitoszam)
                .ThenBy(x => x.Id);
        }


    }
}