using Adatbazis.Models;

namespace Lekerdezesek.Teszt
{
    [TestClass]
    public class UnitTestBejelentesek
    {
        private List<Bejelentes> bejelentesek;
        private BejelentesLekerdezesek bejelentesLekerdezesek;

        [TestInitialize]
        public void Setup()
        {
            var bejelentesek = new List<Bejelentes>
            {
                new Bejelentes
                {
                    Iranyitoszam = "6600",
                    Varos = "Szentes",
                    Cim = "Kossuth u. 40.",
                    HibaLeiras = "Nem világít a lámpa",
                    BejelentesDatuma = new DateTime(2023, 08, 01)
                },

                new Bejelentes
                {
                    Iranyitoszam = "6600",
                    Varos = "Szentes",
                    Cim = "Nagy Ferenc u. 4.",
                    HibaLeiras = "Összevissza villog",
                    BejelentesDatuma = new DateTime(2023, 08, 05)
                },

                new Bejelentes
                {
                    Iranyitoszam = "6640",
                    Varos = "Csongrád",
                    Cim = "Petőfi u. 25.",
                    HibaLeiras = "Sötét van az utcán",
                    BejelentesDatuma = new DateTime(2023, 08, 02)
                },

                new Bejelentes
                {
                    Iranyitoszam = "6635",
                    Varos = "Szegvár",
                    Cim = "Petőfi u. 15.",
                    HibaLeiras = "Nem világít",
                    BejelentesDatuma = new DateTime(2023, 08, 12)
                },

                new Bejelentes
                {
                    Iranyitoszam = "6630",
                    Varos = "Mindszent",
                    Cim = "Rákóczi u. 10.",
                    HibaLeiras = "Kidőlt egy oszlop",
                    BejelentesDatuma = new DateTime(2023, 08, 12),
                    JavitasDatuma = new DateTime(2023, 08, 14),
                    DolgozoId = 1,
                    Dolgozo = new Dolgozo { Id = 1, VezetekNev = "Kovács", KeresztNev = "Ferenc"},
                    JavitasTipusId = 1,
                    JavitasTipus = new JavitasTipus { Id = 1, TipusNev = "Vezeték javítása" }
                }
            };

            bejelentesLekerdezesek = new BejelentesLekerdezesek(bejelentesek);
        }

        [TestMethod]
        public void NyitottBejelentesekListaja()
        {
            var nyitottBejelentesek = bejelentesLekerdezesek.NyitottBejelentesek();

            Assert.AreEqual(4, nyitottBejelentesek.Count());
        }

        [TestMethod]
        public void BejelentesekListajaIranyitoszamAlapjan()
        {
            var szentesiBejelentesek = bejelentesLekerdezesek.BejelentesekIranyitoszamAlapjan("6600");

            Assert.AreEqual(2, szentesiBejelentesek.Count());
        }

        [TestMethod]
        public void BejelentesekListajaIranyitoszamKezdetAlapjan()
        {
            var bejelentesek663x = bejelentesLekerdezesek.BejelentesekIranyitoszamKezdetAlapjan("663");

            Assert.AreEqual(1, bejelentesek663x.Count());
        }

        [TestMethod]
        public void BejelentesekListajaNapokAlapjan()
        {
            var regiBejelentesek = bejelentesLekerdezesek.BejelentesekNapokAlapjan(new DateTime(2023, 8, 31), 20);

            Assert.AreEqual(3, regiBejelentesek.Count());
        }
    }
}