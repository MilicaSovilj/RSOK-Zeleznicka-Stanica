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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-TG65ES3\SQLEXPRESS;Initial Catalog=Zeleznicka__Stanica;Integrated Security=True");

        public void ClData()
        {
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtEmail.Text = "";
            txtKorisnickoIme.Text = "";
            txtLozinka.Password = "";
        }

        public Register()
        {
            InitializeComponent();
        }

        private void txtRegistracija_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "INSERT INTO Korisnik (Prezime, Ime, Email, Username, Pass) VALUES(@Prezime, @Ime, @Email, @Username, @Pass)";
                string query1 = "SELECT COUNT(*) FROM Korisnik WHERE Username = @usename";

                SqlCommand sqlCmdUsr = new SqlCommand(query1, sqlCon);
                sqlCmdUsr.CommandType = CommandType.Text;
                sqlCmdUsr.Parameters.AddWithValue("@usename", txtKorisnickoIme.Text);
                int count = Convert.ToInt32(sqlCmdUsr.ExecuteScalar());
                if (count == 1)
                {
                    MessageBox.Show("Nalog sa istim korisnickim imenom vec postoji. \n Da bi ste se registrovali promenite korisnicko ime");
                    ClData();
                }
                else
                {
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Username", txtKorisnickoIme.Text);
                    sqlCmd.Parameters.AddWithValue("@Pass", txtLozinka.Password);
                    sqlCmd.Parameters.AddWithValue("@Ime", txtIme.Text);
                    sqlCmd.Parameters.AddWithValue("@Prezime", txtPrezime.Text);
                    sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    int countNew = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    MessageBox.Show("Uspesno ste se registrovali");

                    Login dashboard = new Login();
                    dashboard.Show();
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            Login dashboard = new Login();
            dashboard.Show();
            this.Close();
        }
    }
}
