using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace G4_EmployeeRegister.Models
{
    public class UsuarioModel : INotifyPropertyChanged
    {
        // PROPIEDADES
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

        // CONSTRUCTOR
        public UsuarioModel(int idUsuario, string nombre,string apellidos, 
            string email, string username, string contrasenia, BitmapImage foto,
            string rol, string departamento)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Apellidos = apellidos;
            Email = email;
            Username = username;
            Contrasenia = contrasenia;
            Foto = foto;
            Rol = rol;
            Departamento = departamento;
        }

        #region GETTERS Y SETTERS
        public int IdUsuario
        {
            get => _idUsuario;
            set
            {
                if (_idUsuario != value)
                {
                    _idUsuario = value;
                    OnPropertyChanged(nameof(IdUsuario));
                }
            }
        }
        public string NombreCompleto
        {
            get => Nombre+" "+ Apellidos;
        }

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }

        public string Apellidos
        {
            get => _apellidos;
            set
            {
                if (_apellidos != value)
                {
                    _apellidos = value;
                    OnPropertyChanged(nameof(Apellidos));
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Contrasenia
        {
            get => _contrasenia;
            set
            {
                if (_contrasenia != value)
                {
                    _contrasenia = value;
                    OnPropertyChanged(nameof(Contrasenia));
                }
            }
        }

        public BitmapImage Foto
        {
            get => _foto;
            set
            {
                if (_foto != value)
                {
                    _foto = value;
                    OnPropertyChanged(nameof(Foto));
                }
            }
        }

        public string Rol
        {
            get => _rol;
            set
            {
                if (_rol != value)
                {
                    _rol = value;
                    OnPropertyChanged(nameof(Rol));
                }
            }
        }

        public string Departamento
        {
            get => _departamento;
            set
            {
                if (_departamento != value)
                {
                    _departamento = value;
                    OnPropertyChanged(nameof(Departamento));
                }
            }
        }
        #endregion

        // NOTIFICACIÓN DE PROPIEDADES
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
