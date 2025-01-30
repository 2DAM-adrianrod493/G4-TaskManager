using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace G4_EmployeeRegister.ViewModels
{


    class AdminViewModel : INotifyPropertyChanged
    {
        private readonly Services.UsuarioService _usuariosService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<UsuarioModel> Usuarios { get; set; }

        #region PROPIEDADES DE USUARIOS
        private string _nombre, _id, _rol;
        public string Nombre
        {
            get { return _nombre; }
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

        #region PÁGINA EDICIÓN
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

        #region CONTROL VISIBILIDAD
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

        private Visibility _productFrameVisibility = Visibility.Visible;
        public Visibility ProductFrameVisibility
        {
            get => _productFrameVisibility;
            set
            {
                _productFrameVisibility = value;
                OnPropertyChanged(nameof(ProductFrameVisibility));
            }
        }
        #endregion

        #region COMANDOS
        public RelayCommand AddUserCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand VolverAtrasCommand { get; }
        #endregion

        public AdminViewModel()
        {

            _usuariosService = new Services.UsuarioService();
            Usuarios = new ObservableCollection<UsuarioModel>();

            AddUserCommand = new RelayCommand(
                _ => AddUsuario(),
                _ => true
                );

            DeleteCommand = new RelayCommand(
                paramProducto => DeleteUser(paramProducto),
                _ => true
            );

            EditCommand = new RelayCommand(
                paramProducto => EditProduct(paramProducto),
                _ => true
            );

            VolverAtrasCommand = new RelayCommand(
                _ => VolverAtras(),
                _ => EditPage != null
            );

            LoadData();

            LoadUsers();

        }

        #region MÉTODOS
        private void EditUser(object objprodd)
        {

            UsuarioModel usuario = (UsuarioModel)objprodd;

            UsuarioViewModel framePadre = this;

            EditPage = new G4_EmployeeRegister.Views.EditUsuarioView(producto, framePadre, _usuarioServices);

            EditFrameVisibility = Visibility.Visible;
            ProductFrameVisibility = Visibility.Hidden;

        }

        private void DeleteUser(object usuario)
        {
            UsuarioModel usuarioEliminar = (UsuarioModel)usuario;

            var confirmacion = MessageBox.Show("¿Desea eliminar el producto?",
              "Alerta", MessageBoxButton.OKCancel);
            if (confirmacion == MessageBoxResult.OK)
            {
                _usuarioServices.RemoveProduct(usuarioEliminar);
            }
        }

        private void AddUser()
        {
            int id = Usuarios.Count + 1;
            UsuarioModel producto = new UsuarioModel(Id, Nombre, Rol);
            Usuarios.Add(producto);
        }

        public void VolverAtras()
        {
            EditPage = null;
            EditFrameVisibility = Visibility.Hidden;
            ProductFrameVisibility = Visibility.Visible;
        }

        private void LoadData()
        {
            Usuarios = _usuarioServices.GetAllUsuarios();
        }
        #endregion

        private void LoadUsers()
        {
            Usuarios = new UsuarioService().GetAllUsuarios();
        }

        #region EVENTOS DE NOTIFICACIÓN
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

    }
}
