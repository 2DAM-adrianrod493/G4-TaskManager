using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using G4_EmployeeRegister.Views;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Controls;


namespace G4_EmployeeRegister.ViewModels
{
    class AdminViewModel : INotifyPropertyChanged
    {
        private readonly UsuarioService _usuariosService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<UsuarioModel> Usuarios { get; set; }

        #region PROPIEDADES DEL USUARIO (FORMULARIO)
        private string _nombreCompleto;
        private string _nombre;
        private string _apellidos;
        private string _email;
        private string _username;
        private BitmapImage _foto;
        private string _rol;
        private string? _departamento;

        public string NombreCompleto { get => _nombreCompleto; set { _nombreCompleto = value; OnPropertyChanged(nameof(NombreCompleto)); } }
        public string Nombre { get => _nombre; set { _nombre = value; OnPropertyChanged(nameof(Nombre)); } }
        public string Apellidos { get => _apellidos; set { _apellidos = value; OnPropertyChanged(nameof(Apellidos)); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(nameof(Email)); } }
        public string Username { get => _username; set { _username = value; OnPropertyChanged(nameof(Username)); } }
        public BitmapImage Foto { get => _foto; set { _foto = value; OnPropertyChanged(nameof(Foto)); } }
        public string Rol { get => _rol; set { _rol = value; OnPropertyChanged(nameof(Rol)); } }
        public string? Departamento { get => _departamento; set { _departamento = value; OnPropertyChanged(nameof(Departamento)); } }
        #endregion

        #region PROPIEDAD USUARIO SELECCIONADO
        private UsuarioModel _usuarioSeleccionado;
        public UsuarioModel UsuarioSeleccionado
        {
            get => _usuarioSeleccionado;
            set
            {
                _usuarioSeleccionado = value;
                OnPropertyChanged(nameof(UsuarioSeleccionado));
            }
        }
        #endregion

        #region PROPIEDAD BUSCADOR
        private string _buscador;
        public string Buscador
        {
            get => _buscador;
            set
            {
                _buscador = value;
                OnPropertyChanged(nameof(Buscador));
                CollectionViewSource.GetDefaultView(Usuarios)?.Refresh();
            }
        }
        #endregion



        // Cargamos Usuarios
        private void CargarUsuarios()
        {
            Usuarios = _usuariosService.GetAllUsuarios();
        }

        // Método BUSCADOR
        private bool FiltroUsuarios(object obj)
        {
            if (string.IsNullOrEmpty(Buscador))
                return true;

            if (obj is UsuarioModel usuario)
            {
                return usuario.Username.IndexOf(Buscador, StringComparison.OrdinalIgnoreCase) >= 0;
            }

            return false;
        }

        #region COMANDOS
        public RelayCommand AddUser { get; }
        public RelayCommand EditUser { get; }
        public RelayCommand DeleteUser { get; }
        public RelayCommand MostrarFichajes { get; }
        public RelayCommand SeleccionarImagenCommand { get; }
        public RelayCommand VolverAtrasCommand { get; }
        public RelayCommand VolverALogin { get; }
        #endregion

        // CONSTRUCTOR
        public AdminViewModel(UsuarioModel usuario)
        {
            // Inicializamos los valores del usuario actual
            NombreCompleto = usuario.Nombre + " " + usuario.Apellidos;
            _usuariosService = new UsuarioService();
            Usuarios = new ObservableCollection<UsuarioModel>();

            // Comandos
            AddUser = new RelayCommand(_ => AgregarUsuario(), _ => (Usuarios.Count() != 0));
            EditUser = new RelayCommand(_ => EditarUsuario(), _ => (Usuarios.Count() != 0));
            DeleteUser = new RelayCommand(_ => EliminarUsuario(), _ => true);
            MostrarFichajes = new RelayCommand(param => VerFichajes(param), _ => true);
            SeleccionarImagenCommand = new RelayCommand(_ => CargarImagen(), _ => true);
            VolverALogin = new RelayCommand(_ => VolverLoginVentana(), _ => true);


            // Cargamos los usuarios y asignamos el filtro para el buscador
            CargarUsuarios();
            CollectionViewSource.GetDefaultView(Usuarios).Filter = FiltroUsuarios;
        }

        public bool PuedeAniadir()
        {
            if (string.IsNullOrEmpty(NombreCompleto)
                || string.IsNullOrEmpty(Apellidos)
                || string.IsNullOrEmpty(Email)
                || string.IsNullOrEmpty(Departamento)
                || string.IsNullOrEmpty(Rol)
                || Foto == null)
            {
                return false;

            }

            return true;
        }

        public void VolverLoginVentana()
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            Application.Current.Windows[0].Close();
        }

        byte[] usuarioImg;
        private bool imagenCargada = false;

        // Cargamos la imagen seleccionada
        public void CargarImagen()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imágenes (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";

            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    string archivoSeleccionado = openFileDialog.FileName;
                    usuarioImg = File.ReadAllBytes(archivoSeleccionado);
                    Foto = new BitmapImage(new Uri(archivoSeleccionado));
                    imagenCargada = true;
                    MessageBox.Show("Imagen cargada correctamente!", "IMAGEN", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la imagen: {ex.Message}", "ERROR", MessageBoxButton.OK);
            }
        }

        // MÉTODO para ver fichajes de un usuario
        public void VerFichajes(object param)
        {
            if (param is UsuarioModel usuario)
            {
                PaginaFichaje = new VerFichajesPage(usuario);
                PaginaFichajeVisibilty = Visibility.Visible;
                ListadoVisual = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Seleccione un usuario", "AVISO", MessageBoxButton.OK);
            }
        }

        // MÉTODO para eliminar un usuario
        private void EliminarUsuario()
        {
            var confirmacion = MessageBox.Show("¿Quieres eliminar el usuario " + UsuarioSeleccionado.Username + " ?",
                "ELIMINAR", MessageBoxButton.OKCancel);

            if (confirmacion == MessageBoxResult.OK)
            {
                _usuariosService.RemoveUsuario(UsuarioSeleccionado);
                Usuarios.Remove(UsuarioSeleccionado);
            }
        }

        // MÉTODO para editar un usuario
        private void EditarUsuario()
        {
            if (UsuarioSeleccionado != null)
            {
                UsuarioSeleccionado.Nombre = Nombre;
                UsuarioSeleccionado.Apellidos = Apellidos;
                UsuarioSeleccionado.Email = Email;
                UsuarioSeleccionado.Username = Username;
                UsuarioSeleccionado.Rol = Rol;
                UsuarioSeleccionado.Departamento = Departamento;

                _usuariosService.UpdateUsuario(UsuarioSeleccionado);
                OnPropertyChanged(nameof(Usuarios));
            }
        }

        // MÉTODO para agregar un usuario
        public void AgregarUsuario()
        {
            int id = Usuarios.Count() + 1;
            string contrasenia = "$2b$12$9Z6CSQpaRPTSKqUQaGj09.ZL7m8GtWjrGfd3M9bcshsh6yurse7NC";

            if (Usuarios.Any(user => user.Username.Equals(Username)))
            {
                MessageBox.Show("USERNAME YA EN USO");
            }
            else
            {
                Rol = Rol.EndsWith("Usuario") ? "Usuario" : "Administrador";

                UsuarioModel usuario = new UsuarioModel(id, Nombre, Apellidos, Email, Username, contrasenia, Foto, Rol, Departamento?.ToUpper());
                _usuariosService.AddUsuario(usuario);
                Usuarios.Add(usuario);
            }
        }

        #region CONTROL DE VISIBILIDAD
        private Visibility _paginaFichajeVisibilty = Visibility.Hidden;
        public Visibility PaginaFichajeVisibilty
        {
            get => _paginaFichajeVisibilty;
            set { _paginaFichajeVisibilty = value; OnPropertyChanged(nameof(PaginaFichajeVisibilty)); }
        }

        private Visibility _listadoVisual = Visibility.Visible;
        public Visibility ListadoVisual
        {
            get => _listadoVisual;
            set { _listadoVisual = value; OnPropertyChanged(nameof(ListadoVisual)); }
        }
        #endregion

        #region PÁGINA DE EDICIÓN
        private Page _paginaFichaje;
        public Page PaginaFichaje
        {
            get => _paginaFichaje;
            set { _paginaFichaje = value; OnPropertyChanged(nameof(PaginaFichaje)); }
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
