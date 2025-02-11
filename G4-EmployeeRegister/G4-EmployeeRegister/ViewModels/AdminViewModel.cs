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

namespace G4_EmployeeRegister.ViewModels
{
    class AdminViewModel : INotifyPropertyChanged
    {
        private readonly Services.UsuarioService _usuariosService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<UsuarioModel> Usuarios { get; set; }

        #region PROPIEDADES DE USUARIOS
        private string _nombre, _id, _rol, _apellidos;

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
        public string Nombre
        {
            get { return _nombre + " "+ Apellidos; }
            set
            {
                _nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Rol
        {
            get
            {
                return _rol;
            }
            set
            {
                _rol = value;
            }
        }
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
            UsuarioModel usuario = new UsuarioModel(id, Nombre, null,null,null,null,null,null,null,null);
            Usuarios.Add(usuario);
        }
    }
}
