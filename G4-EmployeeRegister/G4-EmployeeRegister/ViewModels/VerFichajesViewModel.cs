﻿using G4_EmployeeRegister.Models;
using G4_EmployeeRegister.Services;
using G4_EmployeeRegister.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace G4_EmployeeRegister.ViewModels
{
    public class VerFichajesViewModel : INotifyPropertyChanged
    {
        private readonly FichajeService _fichajeService;
        private UsuarioModel _usuario;

        // Propiedad que contiene los fichajes de un usuario
        private ObservableCollection<FichajeModel> _fichajes;
        public ObservableCollection<FichajeModel> Fichajes
        {
            get => _fichajes;
            set
            {
                _fichajes = value;
                OnPropertyChanged(nameof(Fichajes));
            }
        }
        public string NombreCompleto { get => _usuario.Nombre + " " + _usuario.Apellidos; }


        public VerFichajesViewModel(UsuarioModel usuario)
        {
            _usuario = usuario;
            _fichajeService = new FichajeService();
            // Cargamos los fichajes del usuario a la propiedad
            Fichajes = new ObservableCollection<FichajeModel>();
            VolverAtrasCommand = new RelayCommand(_ => VolverAtras(), _ => true);
            loadFichajes();
        }

        private void loadFichajes()
        {
            
            Fichajes = new ObservableCollection<FichajeModel>(_fichajeService.GetAllFichajes(_usuario));
        }

        #region CommandoVolverAtras

        public RelayCommand VolverAtrasCommand { get; }
        #endregion
        public void VolverAtras()
        {
            AdminView adminView = new AdminView(_usuario);
            adminView.Show();
            Application.Current.Windows[0].Close();

        }

        #region Página de Edición
        private Page _paginaFichaje;
        public Page PaginaAdmin
        {
            get => _paginaFichaje;
            set
            {
                _paginaFichaje = value;
                OnPropertyChanged(nameof(PaginaAdmin));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}