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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string role = "Admin";
        public int UrID;

        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-TG65ES3\SQLEXPRESS;Initial Catalog=Zeleznicka__Stanica;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
        }
        public void btnLogovanje_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string queryAdmin = "SELECT COUNT(*) FROM Korisnik WHERE Username = @USERNAME AND Pass = @PASSWORD AND Role = '" + role + "'";
                string queryUsr = "SELECT COUNT(*),IDKorisnika FROM Korisnik WHERE Username = @USERNAME AND Pass = @PASSWORD GROUP BY IDKorisnika";
                string queryID = "SELECT IDKorisnika FROM Korisnik WHERE Username = @USERNAME AND Pass = @PASSWORD ";

                SqlCommand sqlCmdAdmin = new SqlCommand(queryAdmin, sqlCon);
                sqlCmdAdmin.CommandType = CommandType.Text;
                sqlCmdAdmin.Parameters.AddWithValue("@USERNAME", txtUsername.Text);
                sqlCmdAdmin.Parameters.AddWithValue("@PASSWORD", txtPassword.Password);
                int count = Convert.ToInt32(sqlCmdAdmin.ExecuteScalar());
                if (count == 1)
                {
                    ZeleznickaStanicaMain dashboard = new ZeleznickaStanicaMain();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    SqlCommand sqlCmdUsr = new SqlCommand(queryUsr, sqlCon);
                    sqlCmdUsr.CommandType = CommandType.Text;
                    sqlCmdUsr.Parameters.AddWithValue("@USERNAME", txtUsername.Text);
                    sqlCmdUsr.Parameters.AddWithValue("@PASSWORD", txtPassword.Password);
                    int countUsr = Convert.ToInt32(sqlCmdUsr.ExecuteScalar());
                    if (countUsr == 1)
                    {
                        SqlCommand sqlCmdID = new SqlCommand(queryID, sqlCon);
                        sqlCmdID.CommandType = CommandType.Text;
                        sqlCmdID.Parameters.AddWithValue("@USERNAME", txtUsername.Text);
                        sqlCmdID.Parameters.AddWithValue("@PASSWORD", txtPassword.Password);
                        UrID = Convert.ToInt32(sqlCmdID.ExecuteScalar());

                        KorisnickiPregled dashboard = new KorisnickiPregled(UrID);
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("User name ili password nisu dobro uneti. Pokusajte ponovo.");
                    }
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

        public void btnRegistracija_Click(object sender, RoutedEventArgs e)
        {
            Register dashboard = new Register();
            dashboard.Show();
            this.Close();
        }
    }
}
