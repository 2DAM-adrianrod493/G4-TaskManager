using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using G4_EmployeeRegister.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private string _nombreCompleto;
        private string _nombre;
        private string _apellidos;
        private string _email;
        private string _username;
        private BitmapImage _foto;
        private string _rol;
        private string? _departamento;

        public string NombreCompleto { get => _nombreCompleto; set => _nombreCompleto = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public string Email { get => _email; set => _email = value; }
        public string Username { get => _username; set => _username = value; }
        public BitmapImage Foto { get => _foto; set => _foto = value; }
        public string Rol { get => _rol; set => _rol = value; }
        public string? Departamento { get => _departamento; set => _departamento = value; }
        #endregion

        #region Propiedad Usuario seleccionado
        private UsuarioModel _usuarioSeleccionado;
        public UsuarioModel UsuarioSeleccionado
        {
            get {
                return _usuarioSeleccionado; 
            }
            set { 
                _usuarioSeleccionado = value;
                OnPropertyChanged(nameof(UsuarioSeleccionado));
            }
        }

        #endregion

        // CARGAMOS USUARIOS
        private void LoadUsers()
        {
            Usuarios = _usuariosService.GetAllUsuarios();
        }

        #region COMANDOS
        // MANEJO DE USUARIOS
        public RelayCommand AddUser { get; }
        public RelayCommand EditUser { get; }
        public RelayCommand DeleteUser { get; }
        public RelayCommand MostrarFichajes { get; }
        #endregion

        public AdminViewModel(UsuarioModel usuario)
        {
            // Inicializamos los valores del usuario actual
            NombreCompleto = usuario.Nombre + " " + usuario.Apellidos;
            _usuariosService = new UsuarioService();
            Usuarios = new ObservableCollection<UsuarioModel>();

            // Acciones Comandos
            AddUser = new RelayCommand(
                _ => AddUsuario(),
                _ => (Usuarios.Count() != 0)
            );

            EditUser = new RelayCommand(_ => EditUsuario(),
                _ => (Usuarios.Count() != 0));

            DeleteUser = new RelayCommand(_ => DeleteUsuario(),
                _ => true);
            MostrarFichajes = new RelayCommand(_=> VerLosFichajes(), _=> true);

            // Cargamos los usuarios
            LoadUsers();
        }

        public void VerLosFichajes()
        {
            if (UsuarioSeleccionado != null)
            {
                Views.VentanFichajes ventanaFichajes = new Views.VentanFichajes(UsuarioSeleccionado);
                ventanaFichajes.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para ver los fichajes.", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // ELIMINAR USUARIO
        private void DeleteUsuario()
        {
          
            var confirmacion = MessageBox.Show("¿Quieres eliminar el usuario " + UsuarioSeleccionado.Username + " ?",
                "ELIMINAR", MessageBoxButton.OKCancel);

            if (confirmacion == MessageBoxResult.OK)
            {
                _usuariosService.RemoveUsuario(UsuarioSeleccionado);
                Usuarios.Remove(UsuarioSeleccionado);
            }
        }

        // EDITAR USUARIO
        private void EditUsuario()
        {
        
        }

        // AÑADIR USUARIO
        public void AddUsuario()
        {
            int id = Usuarios.Count() + 1;
            // CONTRASEÑA POR DEFECTO (1234)
            String Contrasenia = "$2b$12$9Z6CSQpaRPTSKqUQaGj09.ZL7m8GtWjrGfd3M9bcshsh6yurse7NC";

            if (Usuarios.Any(user => user.Username.Equals(Username)))
            {
                MessageBox.Show("USERNAME YA EN USO");
            }
            else
            {
                // Asignamos el rol
                if (Rol.EndsWith("Usuario"))
                {
                    Rol = "Usuario";
                }
                else
                {
                    Rol = "Administrador";
                }

                // Creamos el usuario y lo añadimos al servicio y a la lista
                UsuarioModel usuario = new UsuarioModel(id, Nombre, Apellidos, Email, Username, Contrasenia, Foto, Rol, Departamento.ToUpper());
                _usuariosService.AddUsuario(usuario);
                Usuarios.Add(usuario);
            }
        }

        // VOLVER ATRÁS
        public void VolverAtras()
        {
            EditPage = null;
            EditFrameVisibility = Visibility.Hidden;
            ProductFrameVisibility = Visibility.Visible;
        }

        #region CONTROL DE VISIBILIDAD

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

        #region EVENTO DE NOTIFICACIÓN
        // Método para notificar cambios de propiedad
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
