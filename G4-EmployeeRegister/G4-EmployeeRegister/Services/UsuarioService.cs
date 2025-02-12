using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using G4_EmployeeRegister.Models;
using Microsoft.Data.SqlClient;

namespace G4_EmployeeRegister.Services
{
    public class UsuarioService
    {
        // Creamos Lista Privada
        private ObservableCollection<UsuarioModel> _usuarioList { get; set; }
        private string connectionString = ConfigurationManager.ConnectionStrings["Conexion_App"].ConnectionString;


        // Obtener Listado de Usuarios ya Definidos
        public ObservableCollection<UsuarioModel> GetAllUsuarios()
        {
            _usuarioList = new ObservableCollection<UsuarioModel>();
            UsuarioModel usuario = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT IdUsuario, Nombre, Apellidos, Email, Username,
                            Contrasenia, Rol,
                            Departamento FROM Usuarios;
                        ";
                using (SqlCommand cmdQuery = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = cmdQuery.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idUsuario = Convert.ToInt32(reader["IdUsuario"]);
                            string nombre = reader["Nombre"].ToString();
                            string apellidos = reader["Apellidos"].ToString();
                            string email = reader["Email"].ToString();
                            string username = reader["Username"].ToString();
                            string contrasenia = reader["Contrasenia"].ToString();
                            string rol = reader["Rol"].ToString();
                            string departamento = reader["Departamento"].ToString();

                            usuario = new UsuarioModel(idUsuario, nombre, apellidos, email, username,
                                contrasenia, null, rol, departamento, null);
                            _usuarioList.Add(usuario);
                        }

                    }

                }
                return _usuarioList;
            }
        }


            // Agregar Usuario
            public void AddUsuario(UsuarioModel usuarioModel)
            {
                _usuarioList.Add(usuarioModel);
            }

            // Eliminar Usuario
            public void RemoveUsuario(UsuarioModel usuarioModel)
            {
                _usuarioList.Remove(usuarioModel);
            }

            // Actualizar Usuario
            public void UpdateUsuario(UsuarioModel updatedUsuario)
            {
                // Buscar por ID en la lista
                //var existing = _usuarioList.FirstOrDefault(u => u.IdUsuario == updatedUsuario.IdUsuario);
                //if (existing != null)
                //{
                //    existing.Nombre = updatedUsuario.Nombre;
                //    existing.Apellidos = updatedUsuario.Apellidos;
                //    existing.Email = updatedUsuario.Email;
                //    existing.Username = updatedUsuario.Username;
                //    existing.Contrasenia = updatedUsuario.Contrasenia;
                //    existing.Foto = updatedUsuario.Foto;
                //    existing.Rol = updatedUsuario.Rol;
                //    existing.Departamento = updatedUsuario.Departamento;
                //}
            }
        }
    }
