using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace G4_EmployeeRegister.ViewModels
{
    class UserViewModel : INotifyPropertyChanged
    {
        private readonly Services.FichajeService _fichajeServices;
        public ObservableCollection<FichajeModel> Fichajes { get; set; }

        public UserViewModel(UsuarioModel usuario)
        {
            _fichajeServices = new FichajeService();
            Fichajes = new ObservableCollection<FichajeModel>();
            LoadUsers(usuario);
            
        }

        #region PROPIEDADES DE USUARIOS
        // Propiedad
        private int _idUsuario;
        private string _nombre;
        private string _apellidos;
        private string _email;
        private string _username;
        private string _contrasenia;
        private BitmapImage _foto;
        private string _rol;
        private string? _departamento;
        private FichajeModel _fichaje;

        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string NombreCompleto { get => _nombre + " " + Apellidos; set => _nombre = value; }

        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public string Email { get => _email; set => _email = value; }
        public string Username { get => _username; set => _username = value; }
        public string Contrasenia { get => _contrasenia; set => _contrasenia = value; }
        public BitmapImage Foto { get => _foto; set => _foto = value; }
        public string Rol { get => _rol; set => _rol = value; }
        public string? Departamento { get => _departamento; set => _departamento = value; }
        public FichajeModel Fichaje { get => _fichaje; set => _fichaje = value; }



        #endregion

        private void LoadUsers(UsuarioModel usuario)
        {
            
            foreach(FichajeModel f in Fichajes)
            {

            }

            //hecho por mi para que se ve que he entendido que hace
            var allFichajes = _fichajeServices.GetAllFichajes(usuario);
            var filteredFichajes = new List<FichajeModel>();

            foreach (var f in allFichajes)
            {
                if (f.IdUsuario == usuario.IdUsuario)
                {
                    filteredFichajes.Add(f);
                }
            }

            Fichajes = new ObservableCollection<FichajeModel>(filteredFichajes);
            Apellidos = usuario.Apellidos;
            NombreCompleto = usuario.Nombre;

        }

        #region Métodos de Notificación (INotifyPropertyChanged)

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
