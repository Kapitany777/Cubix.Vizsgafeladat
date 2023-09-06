using Adatbazis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lekerdezesek.Teszt
{
    [TestClass]
    public class UnitTestJavitasTipusok
    {
        [TestMethod]
        public void UresJavitasTipusListaLekerdezese()
        {
            var javitasTipusok = new List<JavitasTipus>();
            var javitasTipusLekerdezesek = new JavitasTipusLekerdezesek(javitasTipusok);

            var eredmeny = javitasTipusLekerdezesek.JavitasTipusokListaja();

            Assert.AreEqual(0, eredmeny.Count());
        }

        [TestMethod]
        public void NemUresJavitasTipusListaLekerdezese()
        {
            var javitasTipusok = new List<JavitasTipus>
            {
                new JavitasTipus { Id = 1, TipusNev = "Kábel" },
                new JavitasTipus { Id = 2, TipusNev = "Lámpabura" }
            };

            var javitasTipusLekerdezesek = new JavitasTipusLekerdezesek(javitasTipusok);

            var eredmeny = javitasTipusLekerdezesek.JavitasTipusokListaja();

            Assert.AreEqual(2, eredmeny.Count());
            Assert.AreEqual(1, eredmeny.First().Id);
            Assert.AreEqual(2, eredmeny.Last().Id);
        }
    }
}
