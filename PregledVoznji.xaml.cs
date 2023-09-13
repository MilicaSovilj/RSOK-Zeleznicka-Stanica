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
    /// Interaction logic for PregledVoznji.xaml
    /// </summary>
    public partial class PregledVoznji : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-TG65ES3\SQLExpress;Initial Catalog=Zeleznicka__Stanica;Integrated Security=True");


        public PregledVoznji()
        {
            InitializeComponent();
            LoadDataGrid();
        }

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
            sqlCon.Close();
        }

        private void LoadDataGrid(string upit)
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Voznja";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Voznja");
            dataAdapter.Fill(dataTable);
            VoznjeDataGrid.ItemsSource = new DataView(dataTable);
            sqlCon.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ZeleznickaStanicaMain dashboard = new ZeleznickaStanicaMain();
            dashboard.Show();
            this.Close();
        }


       

        public DataSet FilterPodatakaPoDatumuVremenuPolaskuDolasku(string datum,string vreme, string polazak, string dolazak)
        {
            DataSet podaciDataSet = new DataSet();
            string upit = "Select * from REZERVACIJA where datum like '%" + datum + "%' or vreme like '%" + vreme + "%' or polazak like '%" + polazak +"%' or dolazak like '%" + dolazak + "%'";
            podaciDataSet = this.LoadDataGrid(upit);
            return podaciDataSet;
        }




        private void btnPretrazi_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
