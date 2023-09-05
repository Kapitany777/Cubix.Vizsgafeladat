using Adatbazis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lekerdezesek
{
    public class LekerdezesAdatbazis
    {
        public IEnumerable<Dolgozo> Dolgozok { get; }

        public IEnumerable<JavitasTipus> JavitasTipusok { get; }

        public IEnumerable<Bejelentes> Bejelentesek { get; }

        public LekerdezesAdatbazis(IEnumerable<Dolgozo> dolgozok, IEnumerable<JavitasTipus> javitasTipusok, IEnumerable<Bejelentes> bejelentesek)
        {
            Dolgozok = dolgozok;
            JavitasTipusok = javitasTipusok;
            Bejelentesek = bejelentesek;
        }
    }
}
