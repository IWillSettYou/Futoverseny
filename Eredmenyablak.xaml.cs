using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Futoverseny
{
    /// <summary>
    /// Interaction logic for Eredmenyablak.xaml
    /// </summary>
    public partial class Eredmenyablak : Window
    {
        public Eredmenyablak(List<Versenyzo> versenyzok)
        {
            InitializeComponent();
            listBoxEredmenyek.ItemsSource = versenyzok
                .OrderBy(v => v.Idoeredmeny)
                .Select(v => $"{v.Nev} - {v.Idoeredmeny}")
                .ToList();
        }
    }
}
