using Adatbazis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lekerdezesek
{
    public class DolgozoLekerdezesek
    {
        public IEnumerable<Dolgozo> Dolgozok { get; }

        public DolgozoLekerdezesek(IEnumerable<Dolgozo> dolgozok)
        {
            Dolgozok = dolgozok;
        }

        /// <summary>
        /// A dolgozók listájának lekérdezése
        /// </summary>
        /// <returns>A dologozók listája</returns>
        public IEnumerable<Dolgozo> DolgozokListaja()
        {
            return Dolgozok
                .OrderBy(d => d.VezetekNev)
                .ThenBy(d => d.KeresztNev);
        }
    }
}
