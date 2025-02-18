using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace G4_EmployeeRegister.Views
{
    public partial class AdminView : Window
    {
        public AdminView(UsuarioModel usuario)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.AdminViewModel(usuario);
            

        }

    }
}
