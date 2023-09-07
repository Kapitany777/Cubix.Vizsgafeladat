using Adatbazis.Models;

namespace Lekerdezesek
{
    public class JavitasTipusLekerdezesek
    {
        public IEnumerable<JavitasTipus> JavitasTipusok { get; }

        public JavitasTipusLekerdezesek(IEnumerable<JavitasTipus> javitasTipusok)
        {
            JavitasTipusok = javitasTipusok;
        }

        /// <summary>
        /// A javítástípusok listájának lekérdezése
        /// </summary>
        /// <returns>A javítástípusok listája</returns>
        public IEnumerable<JavitasTipus> JavitasTipusokListaja()
        {
            return JavitasTipusok
                .OrderBy(d => d.TipusNev);
        }
    }
}
