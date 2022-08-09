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
using Npgsql;
using System.Data;

namespace Maintenance.pages.admin
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();

            try
            {
                cmbLocation.ItemsSource = null;
                cmbPosition.ItemsSource = null;
                cmbRoles.ItemsSource = null;
                DBConn.db.Open();

                //вывод данных в combobox - подразделения
                string sql1 = "select namelocation from maintenance.location";
                NpgsqlCommand cmd1 = new NpgsqlCommand(sql1, DBConn.db);
                DataTable dt1 = new DataTable();
                dt1.Load(cmd1.ExecuteReader());
                cmbLocation.DisplayMemberPath = "namelocation";
                cmbLocation.ItemsSource = dt1.DefaultView;

                //вывод данных в combobox - должности
                string sql2 = "select nameposition from maintenance.position";
                NpgsqlCommand cmd2 = new NpgsqlCommand(sql2, DBConn.db);
                DataTable dt2 = new DataTable();
                dt2.Load(cmd2.ExecuteReader());
                cmbPosition.DisplayMemberPath = "nameposition";
                cmbPosition.ItemsSource = dt2.DefaultView;

                //вывод данных в combobox - роли
                string sql3 = "select nameroles, id from maintenance.roles";
                NpgsqlCommand cmd3 = new NpgsqlCommand(sql3, DBConn.db);
                DataTable dt3 = new DataTable();
                dt3.Load(cmd3.ExecuteReader());
                cmbRoles.DisplayMemberPath = "nameroles";
                cmbRoles.ItemsSource = dt3.DefaultView;

                DBConn.db.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ( txtLogin.Text != "" & txtPassword.Text != "" & txtFIO.Text != "" & cmbLocation.Text != "" & cmbPosition.Text != "" & cmbRoles.Text != "")
            {
                int result = 0;
                try
                {
                    DBConn.db.Open();
                    NpgsqlCommand cmd4 = new NpgsqlCommand(string.Format("select id from maintenance.roles where maintenance.roles.nameroles = '{0}'", cmbRoles.Text), DBConn.db);
                    var dr1 = cmd4.ExecuteReader();
                    dr1.Read();
                    Int16 role = dr1.GetInt16(0);
                    DBConn.db.Close();

                    DBConn.db.Open();
                    NpgsqlCommand cmd5 = new NpgsqlCommand(string.Format("select id from maintenance.location where maintenance.location.namelocation = '{0}'", cmbLocation.Text), DBConn.db);
                    var dr2 = cmd5.ExecuteReader();
                    dr2.Read();
                    Int16 location = dr2.GetInt16(0);                 
                    DBConn.db.Close();

                    DBConn.db.Open();
                    NpgsqlCommand cmd6 = new NpgsqlCommand(string.Format("select id from maintenance.position where maintenance.position.nameposition = '{0}'", cmbPosition.Text), DBConn.db);
                    var dr3 = cmd6.ExecuteReader();
                    dr3.Read();
                    Int16 position = dr3.GetInt16(0);
                    DBConn.db.Close();

                    DBConn.db.Open();
                    string sql4 = "insert into maintenance.users( login, password, idroles, fio, idlocation, idposition)values('"+txtLogin.Text+"','"+txtPassword.Text+"','"+role+"','"+txtFIO.Text+"','"+location+"','"+position+"')";
                    NpgsqlCommand cmd7 = new NpgsqlCommand(sql4, DBConn.db);
                    result = (int)cmd7.ExecuteScalar();
                    DBConn.db.Close();

                    if (result == 1)
                    {
                        MessageBox.Show("Пользователь создан!");
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не создан");
                    }

                }
                catch (Exception ex)
                {
                    DBConn.db.Close();
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            else
                MessageBox.Show("Заполните все поля!");

            }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
