using G4_EmployeeRegister.Models;
using System;
using System.Windows;

namespace G4_EmployeeRegister.Views
{
    public partial class AdminView : Window
    {
        public AdminView(UsuarioModel usuario)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.AdminViewModel(usuario);
        }

        //private void BotonSalir_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //    var loginView = new LoginView();
        //    loginView.Show();
        //}

    }
}
