using Adatbazis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lekerdezesek
{
    public class StatisztikaLekerdezesek
    {
        public IEnumerable<Bejelentes> Bejelentesek { get; }

        public StatisztikaLekerdezesek(IEnumerable<Bejelentes> bejelentesek)
        {
            Bejelentesek = bejelentesek;
        }

        public IEnumerable<Bejelentes> BejelentesekDolgozoAlapjan(Dolgozo dolgozo)
        {
            return Bejelentesek
                .Where(b => b.DolgozoId == dolgozo.Id)
                .OrderBy(b => b.Id);
        }

        public IEnumerable<Bejelentes> BejelentesekEvEsHonapAlapjan(int ev, int honap)
        {
            return Bejelentesek
                .Where(b => b.JavitasDatuma?.Year == ev && b.JavitasDatuma?.Month == honap)
                .OrderBy(b => b.Id);
        }

        public IEnumerable<Bejelentes> BejelentesekJavitasTipusSzerint(int ev, int honap)
        {
            return Bejelentesek
                .Where(b => b.JavitasDatuma?.Year == ev && b.JavitasDatuma?.Month == honap)
                .OrderBy(b => b.JavitasTipus?.TipusNev)
                .ThenBy(b => b.Id);
        }
    }
}
