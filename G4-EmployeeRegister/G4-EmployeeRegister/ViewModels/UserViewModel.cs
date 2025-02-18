using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using G4_EmployeeRegister.ViewModels;
using System.ComponentModel;
using G4_EmployeeRegister.Views;

namespace G4_EmployeeRegister.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private readonly FichajeService _fichajeService;
        private UsuarioModel _usuario;

        public ObservableCollection<FichajeModel> Fichajes { get; set; }

        public RelayCommand IniciarJornadaCommand { get; set; }
        public RelayCommand FinalizarJornadaCommand { get; set; }

        public RelayCommand VolverALogin { get; }

        #region CONSTRUCTOR
        public UserViewModel(UsuarioModel usuario)
        {
            _usuario = usuario;
            _fichajeService = new FichajeService();
            Fichajes = new ObservableCollection<FichajeModel>(_fichajeService.GetAllFichajes(usuario));

            // Inicializamos los Comandos
            IniciarJornadaCommand = new RelayCommand(param => IniciarJornada(),_=>true);
            FinalizarJornadaCommand = new RelayCommand(param => FinalizarJornada(), _ => true);
            VolverALogin = new RelayCommand(_ => VolverLoginVentana(), _ => true);
        }
        #endregion

        public string NombreCompleto { get => _usuario.Nombre + " " + _usuario.Apellidos; }
        
        #region MÉTODOS Iniciar y Finalizar Fichaje
        private void IniciarJornada()
        {
            RegistrarFichaje("Entrada");
        }

        private void FinalizarJornada()
        {
            RegistrarFichaje("Salida");
        }
        #endregion

        #region MÉTODOS
        private void RegistrarFichaje(string tipo)
        {
            // Vemos si el último fue de entrada o salida
            if (Fichajes.Count > 0 && Fichajes.Last().Tipo == tipo)
            {
                // Avisamos de que no puede registrarse una salida o entrada dos veces seguidas
                MessageBox.Show($"{tipo.ToUpper()} ya registrada. Registra otra acción.","ERROR",
                    MessageBoxButton.OK);
                return;
            }

            // Creamos el fichaje con la fecha y el tipo
            var nuevoFichaje = new FichajeModel(0, _usuario.IdUsuario, DateTime.Now, tipo, "");

            _fichajeService.AddFichaje(nuevoFichaje);



            // Actualizamos la lista
            Fichajes.Add(nuevoFichaje);
            // Notificamos que la lista ha cambiado
            OnPropertyChanged(nameof(Fichajes));

            MessageBox.Show($"{tipo.ToUpper()} registrada CORRECTAMENTE", "FICHAJE",
                MessageBoxButton.OK);
        }

        public void VolverLoginVentana()
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            Application.Current.Windows[0].Close();
        }
        #endregion

        #region NOTIFICACIÓN
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            // Notificamos que una propiedad ha cambiado, para poder actualizar la lista
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
