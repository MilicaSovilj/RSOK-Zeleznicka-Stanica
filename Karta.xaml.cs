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
    /// Interaction logic for Karta.xaml
    /// </summary>
    public partial class Karta : Window
    {
        public string P, T, F;
        public int IDK;

      

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            KorisnickiPregled dashboard = new KorisnickiPregled(IDK);
            dashboard.Show();
            this.Close();
        }

        public string Destin;

        public Karta(string P, string T, string F, int IDK)
        {
            InitializeComponent();
            this.P = P;
            this.T = T;
            this.F = F;;
            this.IDK = IDK;

            Fill();
        }

        public void Fill()
        {
            Destin = F + " -> " + T ;

            txtPrevoznik.Text = P.ToString();
            txtDest.Text  = Destin;
   
        }
    }
}
