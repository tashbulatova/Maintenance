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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using System.Data;

namespace Maintenance.pages.admin
{
    /// <summary>
    /// Логика взаимодействия для userManagement.xaml
    /// </summary>
    public partial class userManagement : Page
    {
        private DataTable dt;
        private NpgsqlCommand cmd;
        private string sql = null;
        public userManagement()
        {
            InitializeComponent();

            try
            {
                gdrUsers.ItemsSource = null;
                DBConn.db.Open();
                sql = "select maintenance.users.login, maintenance.users.password, maintenance.users.FIO, maintenance.roles.nameroles, maintenance.location.namelocation, maintenance.position.nameposition from maintenance.users, maintenance.roles, maintenance.location, maintenance.position where maintenance.users.idroles = maintenance.roles.id and maintenance.users.idlocation = maintenance.location.id and maintenance.position.id = maintenance.users.idposition";
                cmd = new NpgsqlCommand(sql, DBConn.db);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                gdrUsers.ItemsSource = dt.DefaultView;
                DBConn.db.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
       
        private void gdrUsers_LoadingRow(object sender, DataGridRowEventArgs e)
        {
           
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddUser addusr = new AddUser();
            addusr.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand cmdDelete = new NpgsqlCommand("delete from maintenance.users where id = :id", DBConn.db);
            cmdDelete.Parameters.AddWithValue("id", gdrUsers.SelectedItem[inde])
            cmdDelete.Prepare();
            cmdDelete.Parameters[0].Value = 
        }
    }
}
