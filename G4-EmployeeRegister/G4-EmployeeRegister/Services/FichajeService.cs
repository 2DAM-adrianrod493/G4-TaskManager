using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using G4_EmployeeRegister.Models;

namespace G4_EmployeeRegister.Services
{
    public class FichajeService
    {
        // Creamos Lista Privada
        private ObservableCollection<FichajeModel> _fichajeList { get; set; }

        // Obtener Listado de Fichajes ya Definidos
        public ObservableCollection<FichajeModel> GetAllFichajes()
        {
            _fichajeList = new ObservableCollection<FichajeModel>();

            _fichajeList.Add(new FichajeModel(1, 1, DateTime.Now, "Entrada", "Todo Correcto"));
            _fichajeList.Add(new FichajeModel(2, 1, DateTime.Now, "Salida", "Todo Correcto"));
            _fichajeList.Add(new FichajeModel(3, 2, DateTime.Now, "Entrada", "Todo Correcto"));
            _fichajeList.Add(new FichajeModel(4, 2, DateTime.Now, "Salida", "Todo Correcto"));

            return _fichajeList;
        }


        // Agregar Fichaje
        public void AddFichaje(FichajeModel fichajeModel)
        {
            _fichajeList.Add(fichajeModel);
        }

        // Eliminar Fichaje
        public void RemoveFichaje(FichajeModel fichajeModel)
        {
            _fichajeList.Remove(fichajeModel);
        }

        // Actualizar Fichaje
        public void UpdateFichaje(FichajeModel updatedFichaje)
        {
            // Buscar por ID en la lista
            //var existing = _fichajeList.FirstOrDefault(f => f.IdFichaje == updatedFichaje.IdFichaje);
            //if (existing != null)
            //{
            //    existing.IdUsuario = updatedFichaje.IdUsuario;
            //    existing.FechaHora = updatedFichaje.FechaHora;
            //    existing.Tipo = updatedFichaje.Tipo;
            //    existing.Observaciones = updatedFichaje.Observaciones;
            //}
        }
    }
}
