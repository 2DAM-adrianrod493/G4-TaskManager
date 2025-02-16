using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using G4_EmployeeRegister.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private string _nombre;
        private string _apellidos;
        private string _email;
        private string _username;
        private BitmapImage _foto;
        private string _rol;
        private string? _departamento;


        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public string Email { get => _email; set => _email = value; }
        public string Username { get => _username; set => _username = value; }
        public BitmapImage Foto { get => _foto; set => _foto = value; }
        public string Rol { get => _rol; set => _rol = value; }
        public string? Departamento { get => _departamento; set => _departamento = value; }
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

            DeleteUser = new RelayCommand(paramUsuario => DeleteUsuario(paramUsuario), _ => true);


            LoadUsers();
        }

        private void DeleteUsuario(object usuario)
        {
            UsuarioModel usuarioEliminar = (UsuarioModel)usuario;
            var confirmacion = MessageBox.Show("¿Desea eliminar el producto " + usuarioEliminar.NombreCompleto + " ?",
                "Alerta", MessageBoxButton.OKCancel);
            if (confirmacion == MessageBoxResult.OK)
            {
                _usuariosService.RemoveUsuario(usuarioEliminar);
            }
        }

        private void EditUsuario()
        {

        }

        public void AddUsuario()
        {

            int id = Usuarios.Count() + 1;
            //Establicer por cada nuevo usuario la contaseña por defecto sea 1234 encryptada
            String Contrasenia = "$2b$12$9Z6CSQpaRPTSKqUQaGj09.ZL7m8GtWjrGfd3M9bcshsh6yurse7NC";
            

            if (Usuarios.Any(user => user.Username.Equals(Username)))
            {
                MessageBox.Show("Ya existe un usuario con dicho username");
            }
            else
            {
                UsuarioModel usuario = new UsuarioModel(id, Nombre, Apellidos, Email, Username, Contrasenia, Foto, Rol.GetType().ToString(), Departamento);
                _usuariosService.AddUsuario(usuario);
                Usuarios.Add(usuario);
            }



        }
        // Metodo para VOLVER ATRÁS
        public void VolverAtras()
        {
            EditPage = null;
            EditFrameVisibility = Visibility.Hidden;
            ProductFrameVisibility = Visibility.Visible;
        }
        #region Control de Visibilidad

        // Propiedad para controlar la visibilidad del frame de edición
        private Visibility _editFrameVisibility = Visibility.Hidden;
        public Visibility EditFrameVisibility
        {
            get => _editFrameVisibility;
            set
            {
                _editFrameVisibility = value;
                OnPropertyChanged(nameof(EditFrameVisibility));
            }
        }

        // Propiedad para controlar la visibilidad del frame de productos
        private Visibility _editPageFrameVisibility = Visibility.Visible;
        public Visibility ProductFrameVisibility
        {
            get => _editPageFrameVisibility;
            set
            {
                _editPageFrameVisibility = value;
                OnPropertyChanged(nameof(ProductFrameVisibility));
            }
        }

        #endregion
        #region Página de Edición

        // Propiedad para la página de edición de producto
        private Page _editPage;
        public Page EditPage
        {
            get => _editPage;
            set
            {
                _editPage = value;
                OnPropertyChanged(nameof(EditPage));
            }
        }

        #endregion
    }

}
