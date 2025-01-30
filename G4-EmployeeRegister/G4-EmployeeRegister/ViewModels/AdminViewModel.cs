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
