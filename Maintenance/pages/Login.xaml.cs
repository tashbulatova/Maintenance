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

namespace Maintenance.pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txbLogin.Text != "" & psbPass.Password != "")
            {
                DBConn.db.Open();
                try
                {
                    var cmd = new NpgsqlCommand(string.Format
                    ("select nameroles from maintenance.roles join maintenance.users on (maintenance.roles.id = maintenance.users.idroles) where maintenance.users.login = '{0}';", txbLogin.Text), DBConn.db);
                    var dr1 = cmd.ExecuteReader();
                    dr1.Read();
                    string role = dr1.GetString(0);
                    switch (role)
                    {
                        case "Администратор":
                            {                                
                                NavigationService.GetNavigationService(this).Navigate(new Uri("pages/admin/Admin.xaml", UriKind.RelativeOrAbsolute));
                                break;
                            }
                        case "Мастер/руководитель":
                            {
                                NavigationService.GetNavigationService(this).Navigate(new Uri("pages/Supervisor.xaml", UriKind.RelativeOrAbsolute));
                                break;
                            }
                        case "Персонал по обслуживанию":
                            {
                                NavigationService.GetNavigationService(this).Navigate(new Uri("pages/Users.xaml", UriKind.RelativeOrAbsolute));
                                break;
                            }
                        case "Метролог":
                            {
                                NavigationService.GetNavigationService(this).Navigate(new Uri("pages/Metrologist.xaml", UriKind.RelativeOrAbsolute));
                                break;
                            }
                    }
                }
                catch (NpgsqlException)
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
                DBConn.db.Close();
            }
            else MessageBox.Show("Введите логин и пароль!");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
