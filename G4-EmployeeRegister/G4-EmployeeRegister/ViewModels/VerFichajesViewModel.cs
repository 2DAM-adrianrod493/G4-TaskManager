using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace G4_EmployeeRegister.ViewModels
{
    public class VerFichajesViewModel : INotifyPropertyChanged
    {
        private readonly FichajeService _fichajeService;
        private UsuarioModel _usuario;

        // Propiedad que contiene los fichajes de un usuario
        private ObservableCollection<FichajeModel> _fichajes;
        public ObservableCollection<FichajeModel> Fichajes
        {
            get => _fichajes;
            set
            {
                _fichajes = value;
                OnPropertyChanged(nameof(Fichajes));
            }
        }
        public string NombreCompleto { get => _usuario.Nombre + " " + _usuario.Apellidos; }


        public VerFichajesViewModel(UsuarioModel usuario)
        {
            _usuario = usuario;
            _fichajeService = new FichajeService();
            // Cargamos los fichajes del usuario a la propiedad
            Fichajes = new ObservableCollection<FichajeModel>();
            loadFichajes();
        }

        private void loadFichajes()
        {
            
            Fichajes = new ObservableCollection<FichajeModel>(_fichajeService.GetAllFichajes(_usuario));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}