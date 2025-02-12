using G4_EmployeeRegister.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace G4_EmployeeRegister.Views
{
    public partial class LoginView : Window
    {
        private LoginViewModel _loginViewModel;
        public LoginView()
        {
            InitializeComponent();
            _loginViewModel = new LoginViewModel(this);
            this.DataContext = _loginViewModel;

        }

        /*
        ¿Por qué esto es necesario?
        - PasswordBox no permite binding directo por seguridad.
        - Usamos un evento PasswordChanged para actualizar la propiedad Password en el ViewModel.
         */

        private void PB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _loginViewModel.Password = ((PasswordBox)sender).Password;
        }
    }

}
