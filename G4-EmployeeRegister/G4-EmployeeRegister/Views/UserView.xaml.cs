using G4_EmployeeRegister.Models;
using System;
using System.Windows;

namespace G4_EmployeeRegister.Views
{
    public partial class UserView : Window
    {
        public UserView(UsuarioModel usuario)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.UserViewModel(usuario);
        }

        //private void BotonSalir_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //    var loginView = new LoginView();
        //    loginView.Show();
        //}

    }
}
