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

namespace Maintenance.pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Uri("pages/Login.xaml", UriKind.RelativeOrAbsolute));
        }

      //  private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
       // {
         //   frmNavigate.Content = new pages.admin.userManagement();
        //}

        private void ListViewItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            frmNavigate.Content = new pages.admin.userManagement();
        }
    }
}
