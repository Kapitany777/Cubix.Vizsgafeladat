using Adatbazis;
using Adatbazis.Models;
using Lekerdezesek;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

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
                MessageBox.Show("A dolgozó kódjának megadása kötelező!");
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
                MessageBox.Show($"Hiba történt: {ex.Message}");
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
                MessageBox.Show("Az év megadása kötelező!");
                return;
            }

            if (string.IsNullOrEmpty(TextBoxHonap.Text))
            {
                MessageBox.Show("A hónap megadása kötelező!");
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
                MessageBox.Show($"Hiba történt: {ex.Message}");
            }
        }

        private void ButtonExport1_Click(object sender, RoutedEventArgs e)
        {
            // TODO: ez még nem működik rendesen
            var dbContext = new MindigFenyesDbContext();
            var lekerdezesek = new StatisztikaLekerdezesek(dbContext.Bejelentesek);

            var eredmeny =
                lekerdezesek
                .BejelentesekDolgozoAlapjan(new Dolgozo { Id = 1 })
                .Select(x => x.Id)
                .ToList();

            var fileName = @"c:\proba\20230906\result1.xml";
            var serializer = new XmlSerializer(lekerdezesek.GetType());
            using (TextWriter stream = new StreamWriter(fileName))
            {
                serializer.Serialize(stream, eredmeny);
                stream.Flush();
            }
        }

        private void ButtonExport2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
