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
    /// Interaction logic for Rezervacije.xaml
    /// </summary>
    public partial class Rezervacije : Window
    {
        private int IDK, IDV;

        public Rezervacije()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-TG65ES3\SQLEXPRESS;Initial Catalog=Zeleznicka__Stanica;Integrated Security=True");

        private void LoadDataGrid()
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT a.IDVoznje as IDVoznje, a.NazivPrevoznika, a.Datum, a.Vreme ,a.Polazak, a.Dolazak, k.IDKorisnika as IDKorisnika, (k.Ime+' '+k.Prezime) as Korisnik FROM Voznja a, Rezervacija r, Korisnik k WHERE k.IDKorisnika = r.IDKorisnika AND a.IDVoznje = r.IDVoznje";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Rezervacija");
            dataAdapter.Fill(dataTable);
            RezPregDataGrid.ItemsSource = new DataView(dataTable);
            sqlCon.Close();
        }
        private void RezPregDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                IDV = int.Parse(dr["IDVoznje"].ToString());
                IDK = int.Parse(dr["IDKorisnika"].ToString());
            }
        }

        private void btnDell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM Rezervacija WHERE IDKorisnika = " + IDK + " AND IDVoznje = " + IDV + "";
                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Uspesno obrisana Rezervacija");
                sqlCon.Close();
                LoadDataGrid();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ZeleznickaStanicaMain dashboard = new ZeleznickaStanicaMain();
            dashboard.Show();
            this.Close();

        }
    }
}
