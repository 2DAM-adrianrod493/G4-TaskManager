using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4_EmployeeRegister.ViewModels
{
    class UserViewModel : INotifyPropertyChanged
    {
        private readonly Services.FichajeService _fichajeServices;
        public ObservableCollection<FichajeModel> Fichajes { get; set; }

        public UserViewModel()
        {
            _fichajeServices = new FichajeService();
            Fichajes = new ObservableCollection<FichajeModel>();
            LoadUsers();
        }

        #region Propiedades

        private int _idFichaje;
        private int _idUsuario;
        private DateTime _fechaHora;
        private string _tipo;
        private string _observaciones;

        public int IdFichaje { get => _idFichaje; set => _idFichaje = value; }
        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public DateTime FechaHora { get => _fechaHora; set => _fechaHora = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        public string Observaciones { get => _observaciones; set => _observaciones = value; }

        #endregion

        private void LoadUsers()
        {
            var allFichajes = _fichajeServices.GetAllFichajes();
            var filteredFichajes = allFichajes.Where(f => f.IdUsuario == 1);
            Fichajes = new ObservableCollection<FichajeModel>(filteredFichajes);
        }

        #region Métodos de Notificación (INotifyPropertyChanged)

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
