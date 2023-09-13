using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Prevoznik.xaml
    /// </summary>
    public partial class Prevoznik : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-TG65ES3\SQLEXPRESS;Initial Catalog=Zeleznicka__Stanica;Integrated Security=True");

        public Prevoznik()
        {
            InitializeComponent();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open(); string query = "INSERT INTO Prevoznik (NazivPrevoznika, Adresa)VALUES(@Naziv, @Adresa)"
                 ;
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
                sqlCmd.Parameters.AddWithValue("@Adresa", txtAdresa.Text);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            UpravljanjeVoznjama dashboard = new UpravljanjeVoznjama();
            dashboard.Show();
            this.Close();
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            UpravljanjeVoznjama dashboard = new UpravljanjeVoznjama();
            dashboard.Show();
            this.Close();
        }
    }
}
