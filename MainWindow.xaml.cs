using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Futoverseny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Versenyzo> versenyzok = new List<Versenyzo>();

        public MainWindow()
        {
            InitializeComponent();
            listBoxVersenyzok.ContextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem { Header = "Eredmények mentése" };
            menuItem.Click += MentettLista_Click;
            listBoxVersenyzok.ContextMenu.Items.Add(menuItem);
        }

        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @"../../../futok.txt";
            if (File.Exists(filePath))
            {
                versenyzok.Clear();
                foreach (var sor in File.ReadAllLines(filePath))
                {
                    var adat = sor.Split(';');
                    if (adat.Length == 5)
                    {
                        versenyzok.Add(new Versenyzo(adat[0], adat[1], adat[2], adat[3], adat[4]));
                    }
                }
                listBoxVersenyzok.ItemsSource = versenyzok.Select(v => v.Nev).ToList();
            }
            else
            {
                MessageBox.Show("A fájl nem található!");
            }
        }

        private void ListBoxVersenyzok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxVersenyzok.SelectedIndex != -1)
            {
                var kivalasztott = versenyzok[listBoxVersenyzok.SelectedIndex];
                txtRajtszam.Text = kivalasztott.Rajtszam;
                txtNev.Text = kivalasztott.Nev;
                txtSzuletes.Text = kivalasztott.SzuletesiDatum;
                txtOrszag.Text = kivalasztott.Orszag;
                txtIdoeredmeny.Text = kivalasztott.Idoeredmeny;
            }
        }

        private void MenuItem_Eredmenylista_Click(object sender, RoutedEventArgs e)
        {
            var eredmenyAblak = new Eredmenyablak(versenyzok);
            eredmenyAblak.Show();
        }

        private void MentettLista_Click(object sender, RoutedEventArgs e)
        {
            var rendezettLista = versenyzok.OrderBy(v => v.Idoeredmeny).ToList();
            using (StreamWriter sw = new StreamWriter(@"../../../EREDMENYEK.txt"))
            {
                foreach (var v in rendezettLista)
                {
                    sw.WriteLine($"{v.Nev}; {v.Idoeredmeny}");
                }
            }
            MessageBox.Show("Eredmények mentve!");
        }
    }

    public class Versenyzo
    {
        public string Rajtszam { get; set; }
        public string Nev { get; set; }
        public string SzuletesiDatum { get; set; }
        public string Orszag { get; set; }
        public string Idoeredmeny { get; set; }

        public Versenyzo(string rajtszam, string nev, string szuletesiDatum, string orszag, string idoeredmeny)
        {
            Rajtszam = rajtszam;
            Nev = nev;
            SzuletesiDatum = szuletesiDatum;
            Orszag = orszag;
            Idoeredmeny = idoeredmeny;
        }
    } 
}