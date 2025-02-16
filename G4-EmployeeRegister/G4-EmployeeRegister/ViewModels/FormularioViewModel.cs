using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace G4_EmployeeRegister.ViewModels
{
    class FormularioViewModel : INotifyPropertyChanged
    {

        public UsuarioModel UsuarioEdit { get; set; }

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


        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public string Email { get => _email; set => _email = value; }
        public string Username { get => _username; set => _username = value; }

        public BitmapImage Foto { get => _foto; set => _foto = value; }
        public string Rol { get => _rol; set => _rol = value; }
        public string? Departamento { get => _departamento; set => _departamento = value; }
        #endregion

        #region FRAME PADRE
        private AdminViewModel _framePadre;

        public AdminViewModel FramePadre
        {
            get => _framePadre;
            set
            {
                _framePadre = value;
                OnPropertyChanged(nameof(FramePadre));
            }
        }
        #endregion
        #region COMANDOS
        public RelayCommand AddCommand { get; }

        #endregion
        public FormularioViewModel()
        {

        }

        public FormularioViewModel(UsuarioModel usuario, AdminViewModel frPadre, UsuarioService usuarioServ)
        {
            FramePadre = frPadre;
            UsuarioEdit = usuario;
            Nombre = UsuarioEdit.Nombre;
            Apellidos = UsuarioEdit.Apellidos;
            Email = UsuarioEdit.Email;
            Username = UsuarioEdit.Username;
            Rol = UsuarioEdit.Rol;
            Foto = UsuarioEdit.Foto;
            Departamento = UsuarioEdit.Departamento;
        }
        private void SaveChanges()
        {
            UsuarioEdit.Nombre = Nombre;
            UsuarioEdit.Apellidos = Apellidos;
            UsuarioEdit.Email = Email;
            UsuarioEdit.Username = Username;
            UsuarioEdit.Departamento = Departamento;
            UsuarioEdit.Rol = Rol;
            UsuarioEdit.Foto = Foto;

            FramePadre.VolverAtras();
        }


        #region EVENTOS DE NOTIFICACIÓN
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
