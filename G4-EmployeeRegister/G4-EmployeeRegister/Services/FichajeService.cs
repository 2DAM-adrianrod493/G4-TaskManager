using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using G4_EmployeeRegister.Models;
using Microsoft.Data.SqlClient;

namespace G4_EmployeeRegister.Services
{
    public class FichajeService
    {
        // Creamos Lista Privada
        private ObservableCollection<FichajeModel> _fichajeList { get; set; }
        private string connectionString = ConfigurationManager.ConnectionStrings["Conexion_App"].ConnectionString;

        // Obtener Listado de Fichajes
        public ObservableCollection<FichajeModel> GetAllFichajes(UsuarioModel usuario)
        {

            _fichajeList = new ObservableCollection<FichajeModel>();
            FichajeModel fichaje = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                // Consulta Query
                string query = @"SELECT f.IdFichaje, f.IdUsuario, f.FechaHora, f.Tipo, f.Observaciones 
                                FROM Fichajes f join Usuarios u on f.IdUsuario = u.IdUsuario 
                                Where u.IdUsuario = "+usuario.IdUsuario+";";

                using (SqlCommand cmdQuery = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmdQuery.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idFichaje = Convert.ToInt32(reader["IdFichaje"]);
                            int idUsuario = Convert.ToInt32(reader["IdUsuario"]);
                            DateTime fechaHora = Convert.ToDateTime(reader["FechaHora"]);
                            string tipo = reader["Tipo"].ToString();
                            string observaciones = reader["Observaciones"].ToString();

                            fichaje = new FichajeModel(idFichaje, idUsuario, fechaHora, tipo, observaciones);
                            _fichajeList.Add(fichaje);
                        }
                    }

                }
            }
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
            var existing = _fichajeList.FirstOrDefault(f => f.IdFichaje == updatedFichaje.IdFichaje);
            if (existing != null)
            {
                existing.IdUsuario = updatedFichaje.IdUsuario;
                existing.FechaHora = updatedFichaje.FechaHora;
                existing.Tipo = updatedFichaje.Tipo;
                existing.Observaciones = updatedFichaje.Observaciones;
            }
        }
    }
}
