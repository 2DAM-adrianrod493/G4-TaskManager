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
        // CREAMOS UNA LISTA PRIVADA
        private ObservableCollection<FichajeModel> _fichajeList { get; set; }
        private string connectionString = ConfigurationManager.ConnectionStrings["Conexion_App"].ConnectionString;

        // OBTENEMOS LOS FICHAJES
        public ObservableCollection<FichajeModel> GetAllFichajes(UsuarioModel usuario)
        {

            _fichajeList = new ObservableCollection<FichajeModel>();
            FichajeModel fichaje = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                connection.Open();

                // CONSULTA QUERY
                string query = @"SELECT f.IdFichaje, f.IdUsuario, f.FechaHora, f.Tipo, f.Observaciones 
                                FROM Fichajes f join Usuarios u on f.IdUsuario = u.IdUsuario 
                                Where u.IdUsuario = @IdUsuario;";

                using (SqlCommand cmdQuery = new SqlCommand(query, connection))
                {
                    cmdQuery.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);

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

        

        // AGREGAR FICHAJE
        public void AddFichaje(FichajeModel fichajeModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Fichajes (IdUsuario, FechaHora, Tipo, Observaciones)
                                VALUES (@IdUsuario, @FechaHora, @Tipo, @Observaciones);";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", fichajeModel.IdUsuario);
                    cmd.Parameters.AddWithValue("@FechaHora", fichajeModel.FechaHora);
                    cmd.Parameters.AddWithValue("@Tipo", fichajeModel.Tipo);
                    cmd.Parameters.AddWithValue("@Observaciones", fichajeModel.Observaciones);

                    cmd.ExecuteNonQuery();
                }
            }
            _fichajeList.Add(fichajeModel);
        }
    }
}
