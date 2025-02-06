using G4_EmployeeRegister.Models;
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

        public ObservableCollection<FichajeModel> _fichajes { get; }





        #region Métodos de Notificación (INotifyPropertyChanged)

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

    }
}
