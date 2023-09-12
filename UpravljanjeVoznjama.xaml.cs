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
    /// Interaction logic for UpravljanjeVoznjama.xaml
    /// </summary>
    public partial class UpravljanjeVoznjama : Window
    {

        private int ID;

        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-TG65ES3\SQLEXPRESS;Initial Catalog=Zeleznicka__Stanica;Integrated Security=True");

        private List<string> getPrevoznik()
        {
            List<string> results = new List<string>();
            DataSet Prevoznici = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("SELECT NazivPrevoznika FROM Prevoznik", sqlCon);

                adapter.Fill(Prevoznici);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            foreach (DataRow row in Prevoznici.Tables[0].Rows)
            {
                results.Add((string)row["NazivPrevoznika"]);
            }
            return results;
        }


       


        public UpravljanjeVoznjama()
        {
            InitializeComponent();
            LoadDataGrid();
            prevoznikComboBox.ItemsSource = getPrevoznik();
           
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
            /*LetoviDataGrid.Columns[5].MaxWidth = 0;*/
            sqlCon.Close();
        }

        private void ponistiUnosTxt()
        {
            prevoznikComboBox.SelectedIndex = -1; 
            dateDatumPol.Text = "";
            txtVreme.Text = "";
            txtpocetnaDest.Text = "";
            txtKrajnjaDest.Text = "";
         
        }



        private void btmBack_Click(object sender, RoutedEventArgs e)
        {
            ZeleznickaStanicaMain dashboard = new ZeleznickaStanicaMain();
            dashboard.Show();
            this.Close();
        }

        private void VoznjeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                ID = int.Parse(dr["IDVoznje"].ToString());
                prevoznikComboBox.SelectedValue = dr["NazivPrevoznika"].ToString();
                dateDatumPol.Text = dr["Datum"].ToString();
                txtVreme.Text = dr["Vreme"].ToString();
                txtpocetnaDest.Text = dr["Polazak"].ToString();
                txtKrajnjaDest.Text = dr["Dolazak"].ToString();
                 
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "INSERT INTO Voznja(NazivPrevoznika, Datum, Vreme, Polazak, Dolazak) VALUES(@Naziv, @Datum, @Vreme, @Start, @Odrediste)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Naziv", prevoznikComboBox.SelectedItem);
                sqlCmd.Parameters.AddWithValue("@Datum", dateDatumPol.SelectedDate);
                sqlCmd.Parameters.AddWithValue("@Vreme", txtVreme.Text);
                sqlCmd.Parameters.AddWithValue("@Start", txtpocetnaDest.Text);
                sqlCmd.Parameters.AddWithValue("@Odrediste", txtKrajnjaDest.Text);
                
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                LoadDataGrid();
            }
            ponistiUnosTxt();

        }

        private void btnDell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "DELETE FROM Voznja WHERE IDVoznje = " + ID+ "";
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
                sqlCon.Close();
                LoadDataGrid();
            }
            ponistiUnosTxt();

        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "UPDATE Voznja SET NazivPrevoznika = @Naziv, Datum = @Datum, Vreme = @Vreme ,Polazak = @Start, Dolazak = @Odrediste WHERE IDVoznje = " + ID + "";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Naziv", prevoznikComboBox.SelectedItem);
                sqlCmd.Parameters.AddWithValue("@Datum", dateDatumPol.SelectedDate);
                sqlCmd.Parameters.AddWithValue("@Vreme", txtVreme.Text);
                sqlCmd.Parameters.AddWithValue("@Start", txtpocetnaDest.Text);
                sqlCmd.Parameters.AddWithValue("@Odrediste", txtKrajnjaDest.Text);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    MessageBox.Show("Podaci su uspesno promenjeni");
                    LoadDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                LoadDataGrid();
            }
            ponistiUnosTxt();

        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Prevoznik dashboard = new Prevoznik();
            dashboard.Show();
            this.Close();


        }

      
        private void btnIsprazni_Click(object sender, RoutedEventArgs e)
        {
            ponistiUnosTxt();
        }
    }
}
