using Adatbazis.Models;

namespace Lekerdezesek.Teszt
{
    [TestClass]
    public class UnitTestDolgozok
    {
        [TestMethod]
        public void UresDolgozoListaLekerdezese()
        {
            var dolgozok = new List<Dolgozo>();
            var dolgozoLekerdezesek = new DolgozoLekerdezesek(dolgozok);

            var eredmeny = dolgozoLekerdezesek.DolgozokListaja();

            Assert.AreEqual(0, eredmeny.Count());
        }

        [TestMethod]
        public void NemUresDolgozoListaLekerdezese()
        {
            var dolgozok = new List<Dolgozo>
            {
                new Dolgozo { Id = 1, VezetekNev = "Kovács", KeresztNev = "Ferenc"},
                new Dolgozo { Id = 2, VezetekNev = "Arany", KeresztNev = "János"}
            };

            var dolgozoLekerdezesek = new DolgozoLekerdezesek(dolgozok);

            var eredmeny = dolgozoLekerdezesek.DolgozokListaja();

            Assert.AreEqual(2, eredmeny.Count());
            Assert.AreEqual(2, eredmeny.First().Id);
            Assert.AreEqual(1, eredmeny.Last().Id);
        }
    }
}
