using System;
using System.Windows;

namespace G4_EmployeeRegister.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            this.Closing += LoginView_Closing;
        }

        // Cerrar App
        private void LoginView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ClickAdmin(object sender, RoutedEventArgs e)
        {
            var adminView = new AdminView();
            adminView.Show();
            this.Hide();
        }

        private void ClickUsuario(object sender, RoutedEventArgs e)
        {
            var userView = new UserView();
            userView.Show();
            this.Hide();
        }
    }

}
