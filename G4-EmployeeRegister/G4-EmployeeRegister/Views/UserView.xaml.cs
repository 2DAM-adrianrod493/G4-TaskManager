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

    }
}
