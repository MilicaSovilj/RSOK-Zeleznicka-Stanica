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
    /// Interaction logic for KorisnickiPregled.xaml
    /// </summary>
    public partial class KorisnickiPregled : Window
    {
        private int IDK;
        private int IDV;
        private string Prevoznik;
        private string Od;
        private string Do;
        private string ZaUneti;
      

        public KorisnickiPregled(int iD)
        {
            InitializeComponent();
            LoadDataGrid();
            this.IDK = iD;
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-TG65ES3\SQLEXPRESS;Initial Catalog=Zeleznicka__Stanica;Integrated Security=True");

        private void LoadDataGrid()
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Voznja";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Voznja");
            dataAdapter.Fill(dataTable);
            VoznjeDataGrid.ItemsSource = new DataView(dataTable);
            VoznjeDataGrid.Columns[0].MaxWidth = 0;
            sqlCon.Close();
        }

        private void btnRezervisi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO Rezervacija (IDKorisnika, IDVoznje) VALUES(@IDK, @IDV)";
                if (sqlCon.State == ConnectionState.Closed)sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@IDK", IDK);
                sqlCmd.Parameters.AddWithValue("@IDV", IDV);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            Karta dashboard = new Karta(Prevoznik, Do, Od, IDK);
            dashboard.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login dashboard = new Login();
            dashboard.Show();
            this.Close();
        }

        private void VoznjeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                IDV = int.Parse(dr["IDVoznje"].ToString());
                Prevoznik = dr["NazivPrevoznika"].ToString();
                Od = dr["Polazak"].ToString();
                Do = dr["Dolazak"].ToString();
          
            }

            ZaUneti = Prevoznik + "  ||  " + Od + " -> " + Do;

            txtVoznja.Text = ZaUneti.ToString();
        }
    }
}
