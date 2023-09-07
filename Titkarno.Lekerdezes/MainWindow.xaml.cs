using Adatbazis;
using Adatbazis.Models;
using Lekerdezesek;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Titkarno.Lekerdezes.Serialization;
using Titkarno.Lekerdezes.Utils;

namespace Titkarno.Lekerdezes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MunkatarsAltalElvegzettMunkak(MindigFenyesDbContext dbContext, Dolgozo dolgozo)
        {
            var lekerdezesek = new StatisztikaLekerdezesek(dbContext.Bejelentesek);

            var eredmeny = lekerdezesek.BejelentesekDolgozoAlapjan(dolgozo);

            DataGridEredmeny1.ItemsSource = eredmeny;
        }

        private void ButtonLekerdezes1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxDolgozoId.Text))
            {
                WpfMessageBox.MsgError("A dolgozó kódjának megadása kötelező!");
                return;
            }

            try
            {
                var dbContext = new MindigFenyesDbContext();
                var dolgozo = dbContext.Dolgozok.Find(int.Parse(TextBoxDolgozoId.Text));

                if (dolgozo != null)
                {
                    TextBlockDolgozoNev.Text = $"{dolgozo.VezetekNev} {dolgozo.KeresztNev}";

                    MunkatarsAltalElvegzettMunkak(dbContext, dolgozo);
                }
                else
                {
                    throw new Exception("Hibás dolgozókód!");
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.MsgError("Hiba történt:", ex);
            }
        }

        private void AdottHonapbanElvegzettOsszesMunka(
            MindigFenyesDbContext dbContext,
            int ev,
            int honap,
            Func<int, int, IEnumerable<Bejelentes>> lekerdezes)
        {
            var eredmeny = lekerdezes(ev, honap);

            DataGridEredmeny2.ItemsSource = eredmeny;
        }

        private void ButtonLekerdezes2_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxEv.Text))
            {
                WpfMessageBox.MsgError("Az év megadása kötelező!");
                return;
            }

            if (string.IsNullOrEmpty(TextBoxHonap.Text))
            {
                WpfMessageBox.MsgError("A hónap megadása kötelező!");
                return;
            }

            try
            {
                var dbContext = new MindigFenyesDbContext();

                var lekerdezesek = new StatisztikaLekerdezesek(dbContext.Bejelentesek);

                if (CheckBoxFeladatTipus.IsChecked == true)
                {
                    AdottHonapbanElvegzettOsszesMunka(dbContext, int.Parse(TextBoxEv.Text), int.Parse(TextBoxHonap.Text), lekerdezesek.BejelentesekJavitasTipusSzerint);
                }
                else
                {
                    AdottHonapbanElvegzettOsszesMunka(dbContext, int.Parse(TextBoxEv.Text), int.Parse(TextBoxHonap.Text), lekerdezesek.BejelentesekEvEsHonapAlapjan);
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.MsgError("Hiba történt:", ex);
            }
        }

        private string ExportFileNevBekerese()
        {
            var filenev = string.Empty;

            var dialog = new SaveFileDialog();

            dialog.Filter = "JSON file (*.json)|*.json|XML file|*.xml";

            if (dialog.ShowDialog() == true)
            {
                filenev = dialog.FileName;
            }

            return filenev;
        }

        private EredmenySerializer? GetSerializer(string fileNev)
        {
            EredmenySerializer? serializer = null;

            if (System.IO.Path.GetExtension(fileNev) == ".json")
            {
                serializer = new EredmenyJsonSerializer(fileNev);
            }
            else if (System.IO.Path.GetExtension(fileNev) == ".xml")
            {
                serializer = new EredmenyXmlSerializer(fileNev);
            }

            return serializer;
        }

        private void Eredmeny1Exportalasa(string fileNev)
        {
            EredmenySerializer? serializer = GetSerializer(fileNev);

            if (serializer == null)
            {
                throw new Exception("Hibás fájltípus!");
            }

            var dbContext = new MindigFenyesDbContext();

            var dolgozo = dbContext.Dolgozok.Find(int.Parse(TextBoxDolgozoId.Text));

            if (dolgozo == null)
            {
                throw new Exception("Hibás dolgozókód!");
            }

            var lekerdezesek = new StatisztikaLekerdezesek(dbContext.Bejelentesek);

            var eredmeny = lekerdezesek
                .BejelentesekDolgozoAlapjan(dolgozo)
                .Select(b =>
                    new Eredmeny
                    {
                        Id = b.Id,
                        Iranyitoszam = b.Iranyitoszam,
                        Varos = b.Varos,
                        Cim = b.Cim,
                        HibaLeiras = b.HibaLeiras,
                        BejelentesDatuma = b.BejelentesDatuma,
                        JavitasDatuma = b.JavitasDatuma,
                        DolgozoId = b.DolgozoId,
                        DolgozoNev = $"{b.Dolgozo?.VezetekNev} {b.Dolgozo?.KeresztNev}",
                        JavitasTipusId = b.JavitasTipusId,
                        TipusNev = b.JavitasTipus?.TipusNev
                    })
                .ToList();

            serializer.Serialize(eredmeny);
        }

        private void ButtonExport1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxDolgozoId.Text))
            {
                WpfMessageBox.MsgError("A dolgozó kódjának megadása kötelező!");
                return;
            }

            string fileNev = ExportFileNevBekerese();

            if (string.IsNullOrEmpty(fileNev))
            {
                return;
            }

            try
            {
                Eredmeny1Exportalasa(fileNev);

                WpfMessageBox.MsgInfo("Az exportálás sikeresen befejeződött!");
            }
            catch (Exception ex)
            {
                WpfMessageBox.MsgError("Hiba történt!", ex);
            }
        }

        private void Eredmeny2Exportalasa(
            string fileNev,
            int ev,
            int honap)
        {
            EredmenySerializer? serializer = GetSerializer(fileNev);

            if (serializer == null)
            {
                throw new Exception("Hibás fájltípus!");
            }

            var dbContext = new MindigFenyesDbContext();

            var lekerdezesek = new StatisztikaLekerdezesek(dbContext.Bejelentesek);

            if (CheckBoxFeladatTipus.IsChecked == true)
            {
                var eredmeny = lekerdezesek
                    .BejelentesekJavitasTipusSzerint(ev, honap)
                    .Select(b =>
                        new Eredmeny
                        {
                            Id = b.Id,
                            Iranyitoszam = b.Iranyitoszam,
                            Varos = b.Varos,
                            Cim = b.Cim,
                            HibaLeiras = b.HibaLeiras,
                            BejelentesDatuma = b.BejelentesDatuma,
                            JavitasDatuma = b.JavitasDatuma,
                            DolgozoId = b.DolgozoId,
                            DolgozoNev = $"{b.Dolgozo?.VezetekNev} {b.Dolgozo?.KeresztNev}",
                            JavitasTipusId = b.JavitasTipusId,
                            TipusNev = b.JavitasTipus?.TipusNev
                        })
                    .ToList();

                serializer.Serialize(eredmeny);
            }
            else
            {
                var eredmeny = lekerdezesek
                    .BejelentesekEvEsHonapAlapjan(ev, honap)
                    .Select(b =>
                        new Eredmeny
                        {
                            Id = b.Id,
                            Iranyitoszam = b.Iranyitoszam,
                            Varos = b.Varos,
                            Cim = b.Cim,
                            HibaLeiras = b.HibaLeiras,
                            BejelentesDatuma = b.BejelentesDatuma,
                            JavitasDatuma = b.JavitasDatuma,
                            DolgozoId = b.DolgozoId,
                            DolgozoNev = $"{b.Dolgozo?.VezetekNev} {b.Dolgozo?.KeresztNev}",
                            JavitasTipusId = b.JavitasTipusId,
                            TipusNev = b.JavitasTipus?.TipusNev
                        })
                    .ToList();

                serializer.Serialize(eredmeny);
            }
        }

        private void ButtonExport2_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxEv.Text))
            {
                WpfMessageBox.MsgError("Az év megadása kötelező!");
                return;
            }

            if (string.IsNullOrEmpty(TextBoxHonap.Text))
            {
                WpfMessageBox.MsgError("A hónap megadása kötelező!");
                return;
            }

            string fileNev = ExportFileNevBekerese();

            if (string.IsNullOrEmpty(fileNev))
            {
                return;
            }

            try
            {
                Eredmeny2Exportalasa(fileNev, int.Parse(TextBoxEv.Text), int.Parse(TextBoxHonap.Text));

                WpfMessageBox.MsgInfo("Az exportálás sikeresen befejeződött!");
            }
            catch (Exception ex)
            {
                WpfMessageBox.MsgError("Hiba történt!", ex);
            }
        }
    }
}
