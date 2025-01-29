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

namespace G4_EmployeeRegister.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void ClickAdmin(object sender, RoutedEventArgs e)
        {
            AdminView adminView = new AdminView();
            adminView.ShowDialog();
        }

        private void ClickUsuario(object sender, RoutedEventArgs e)
        {
            UserView userView = new UserView();
            userView.ShowDialog();
        }

    }
}
