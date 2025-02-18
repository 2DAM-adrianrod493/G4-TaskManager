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
        // Lista para Almacenar Fichajes
        private ObservableCollection<FichajeModel> _fichajeList { get; set; }

        // Cadena de Conexión Base de Datos
        private string connectionString = ConfigurationManager.ConnectionStrings["Conexion_App"].ConnectionString;

        // MÉTODO Obtener Fichajes de un Usuario específico
        public ObservableCollection<FichajeModel> GetAllFichajes(UsuarioModel usuario)
        {
            _fichajeList = new ObservableCollection<FichajeModel>();
            FichajeModel fichaje = null;

            // Conexión
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta SQL
                string query = @"SELECT f.IdFichaje, f.IdUsuario, f.FechaHora, f.Tipo, f.Observaciones 
                                 FROM Fichajes f 
                                 JOIN Usuarios u ON f.IdUsuario = u.IdUsuario 
                                 WHERE u.IdUsuario = " + usuario.IdUsuario + ";";

                // Ejecutamos la Consulta
                using (SqlCommand cmdQuery = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmdQuery.ExecuteReader())
                    {
                        // Leemos cada fila del resultado de la consulta
                        while (reader.Read())
                        {
                            int idFichaje = Convert.ToInt32(reader["IdFichaje"]);
                            int idUsuario = Convert.ToInt32(reader["IdUsuario"]);
                            DateTime fechaHora = Convert.ToDateTime(reader["FechaHora"]);
                            string tipo = reader["Tipo"].ToString();
                            string observaciones = reader["Observaciones"].ToString();

                            // Creamos un nuevo objeto tipo FichajeModel y lo agregamos a la lista
                            fichaje = new FichajeModel(idFichaje, idUsuario, fechaHora, tipo, observaciones);
                            
                            _fichajeList.Add(fichaje);
                        }
                    }
                }
            }
            return _fichajeList;
        }

        // MÉTODO Agregar Fichaje a la Base de Datos
        public void AddFichaje(FichajeModel fichajeModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta SQL Insertar Fichaje
                string query = @"INSERT INTO Fichajes (IdUsuario, FechaHora, Tipo, Observaciones)
                                 VALUES (@IdUsuario, @FechaHora, @Tipo, @Observaciones);";

                // Ejecutamos la Consulta
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", fichajeModel.IdUsuario);
                    cmd.Parameters.AddWithValue("@FechaHora", fichajeModel.FechaHora);
                    cmd.Parameters.AddWithValue("@Tipo", fichajeModel.Tipo);
                    cmd.Parameters.AddWithValue("@Observaciones", fichajeModel.Observaciones);

                    // Ejecutamos la Inserción de los Datos
                    cmd.ExecuteNonQuery();
                }
            }
            // Agregamos el Fichaje a la Lista
            _fichajeList.Add(fichajeModel);
        }
    }
}
