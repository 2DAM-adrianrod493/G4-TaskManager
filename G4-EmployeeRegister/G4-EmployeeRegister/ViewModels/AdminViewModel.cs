using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using G4_EmployeeRegister.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
            get
            {
                return _usuarioSeleccionado;
            }
            set
            {
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
        public RelayCommand SeleccionarImagenCommand { get; }
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
            MostrarFichajes = new RelayCommand(paramUsuario => VerLosFichajes(paramUsuario), _ => true);
            SeleccionarImagenCommand = new RelayCommand(_ => CargaImagen(), _ => true);

            // Cargamos los usuarios
            LoadUsers();
        }

        byte[] usuarioImg;
        private bool imagenSabida = false;

        // Cargar la imagen seleccionada por el usuario
        public void CargaImagen()
        {
            // Crear un OpenFileDialog para seleccionar una imagen
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imágenes (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";

            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    string archivoSeleccionado = openFileDialog.FileName;

                    // Aquí guardamos la imagen seleccionada en el arreglo de bytes
                    usuarioImg = File.ReadAllBytes(archivoSeleccionado);

                    // Creamos un BitmapImage para mostrar la imagen en la vista
                    Foto = new BitmapImage(new Uri(archivoSeleccionado));

                    // Marcar que una imagen ha sido cargada
                    imagenSabida = true;

                    // Mensaje de confirmación
                    MessageBox.Show("Imagen cargada correctamente!", "Imagen", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void VerLosFichajes(object paramUser)
        {
            UsuarioModel usuario = (UsuarioModel)paramUser;
            if (usuario != null)
            {
                PaginaFichaje = new Views.VerFichajesPage(usuario);
                PaginaFichajeVisibilty = Visibility.Visible;
                ListadoVisual = Visibility.Hidden;
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
            // Lógica para editar el usuario si es necesario
        }

        // AÑADIR USUARIO
        public void AddUsuario()
        {
            int id = Usuarios.Count() + 1;
            String Contrasenia = "$2b$12$9Z6CSQpaRPTSKqUQaGj09.ZL7m8GtWjrGfd3M9bcshsh6yurse7NC";

            if (Usuarios.Any(user => user.Username.Equals(Username)))
            {
                MessageBox.Show("USERNAME YA EN USO");
            }
            else
            {
                if (Rol.EndsWith("Usuario"))
                {
                    Rol = "Usuario";
                }
                else
                {
                    Rol = "Administrador";
                }

                UsuarioModel usuario = new UsuarioModel(id, Nombre, Apellidos, Email, Username, Contrasenia, Foto, Rol, Departamento?.ToUpper());
                _usuariosService.AddUsuario(usuario);
                Usuarios.Add(usuario);
            }
        }

        //// VOLVER ATRÁS
        //public void VolverAtras()
        //{
        //    EditPage = null;
        //    EditFrameVisibility = Visibility.Hidden;
        //    ProductFrameVisibility = Visibility.Visible;
        //}

        #region CONTROL DE VISIBILIDAD
        private Visibility _paginaFichajeVisibilty = Visibility.Hidden;
        public Visibility PaginaFichajeVisibilty
        {
            get => _paginaFichajeVisibilty;
            set
            {
                _paginaFichajeVisibilty = value;
                OnPropertyChanged(nameof(PaginaFichajeVisibilty));
            }
        }

        private Visibility _listadoVisual = Visibility.Visible;
        public Visibility ListadoVisual
        {
            get => _listadoVisual;
            set
            {
                _listadoVisual = value;
                OnPropertyChanged(nameof(ListadoVisual));
            }
        }
        #endregion

        #region Página de Edición
        private Page _paginaFichaje;
        public Page PaginaFichaje
        {
            get => _paginaFichaje;
            set
            {
                _paginaFichaje = value;
                OnPropertyChanged(nameof(PaginaFichaje));
            }
        }
        #endregion

        #region EVENTO DE NOTIFICACIÓN
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
