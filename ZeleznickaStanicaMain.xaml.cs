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

namespace ZeleznickaStanica
{
    /// <summary>
    /// Interaction logic for ZeleznickaStanicaMain.xaml
    /// </summary>
    public partial class ZeleznickaStanicaMain : Window
    {
        public ZeleznickaStanicaMain()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login dashboard = new Login();
            dashboard.Show();
            this.Close();
        }

        private void btnPregled_Click(object sender, RoutedEventArgs e)
        {
            PregledVoznji dashboard = new PregledVoznji();
            dashboard.Show();
            this.Close();
        }

        private void btnRezervacije_Click(object sender, RoutedEventArgs e)
        {
            Rezervacije dashboard = new Rezervacije();
            dashboard.Show();
            this.Close();
        }

        private void btnUpravljanje_Click(object sender, RoutedEventArgs e)
        {
            UpravljanjeVoznjama dashboard = new UpravljanjeVoznjama();
            dashboard.Show();
            this.Close();
        }
    }
}
