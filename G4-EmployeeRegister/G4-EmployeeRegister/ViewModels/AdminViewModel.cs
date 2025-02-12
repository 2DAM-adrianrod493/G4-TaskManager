using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace G4_EmployeeRegister.ViewModels
{
    class AdminViewModel : INotifyPropertyChanged
    {
        private readonly Services.UsuarioService _usuariosService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<UsuarioModel> Usuarios { get; set; }
      

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
        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public string Email { get => _email; set => _email = value; }
        public string Username { get => _username; set => _username = value; }
        public string Contrasenia { get => _contrasenia; set => _contrasenia = value; }
        public BitmapImage Foto { get => _foto; set => _foto = value; }
        public string Rol { get => _rol; set => _rol = value; }
        public string? Departamento { get => _departamento; set => _departamento = value; }
        public FichajeModel Fichaje { get => _fichaje; set => _fichaje = value; }



        #endregion

        private void LoadUsers()
        {
            Usuarios = _usuariosService.GetAllUsuarios();
        }

        #region EVENTOS DE NOTIFICACIÓN
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region Comandos
        // Comandos para manejo de ususarios
        public RelayCommand AddUser { get; }
        public RelayCommand EditUser { get; }
        public RelayCommand DeleteUser { get; }
        

        #endregion
        public AdminViewModel()
        {
            _usuariosService = new UsuarioService();
            Usuarios = new ObservableCollection<UsuarioModel>();




            AddUser = new RelayCommand(
                _ => AddUsuario(),
                _ => (Usuarios.Count() != 0)

                );
            EditUser = new RelayCommand(_ => EditUsuario(),
                _ => (Usuarios.Count() != 0));

            DeleteUser = new RelayCommand(_ => DeleteUsuario(),
                _ => (Usuarios.Count() != 0));


            LoadUsers();
        }

        private void DeleteUsuario()
        {
            
        }

        private void EditUsuario()
        {
            
        }

        public void AddUsuario()
        {
            int id = Usuarios.Count()+1;
            UsuarioModel usuario = new UsuarioModel(id, Nombre, Apellidos,Email,Username,Contrasenia,Foto,Rol,Departamento,Fichaje);
            Usuarios.Add(usuario);
        }
    }
}
