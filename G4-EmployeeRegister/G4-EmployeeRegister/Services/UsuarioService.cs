using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using G4_EmployeeRegister.Models;
using Microsoft.Data.SqlClient;

namespace G4_EmployeeRegister.Services
{
    public class UsuarioService
    {
        // Lista para Almacenar Usuarios
        private ObservableCollection<UsuarioModel> _usuarioList { get; set; }
        
        // Cadena de Conexión Base de Datos
        private string connectionString = ConfigurationManager.ConnectionStrings["Conexion_App"].ConnectionString;

        // MÉTODO Obtener Usuarios
        public ObservableCollection<UsuarioModel> GetAllUsuarios()
        {
            _usuarioList = new ObservableCollection<UsuarioModel>();
            UsuarioModel usuario = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta SQL
                string query = @"SELECT IdUsuario, Nombre, Apellidos, Email, Username, 
                                Contrasenia,Foto, Rol,
                                Departamento FROM Usuarios;";

                // Ejecutamos la Consulta
                using (SqlCommand cmdQuery = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmdQuery.ExecuteReader())
                    {
                        // Leemos cada fila del resultado de la consulta
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
                            
                            // Manejo de la imagen
                            BitmapImage imagUser = null;

                            // Vemos que no sea null
                            if (reader["Foto"] != DBNull.Value)
                            {
                                // Convertimos el resultado a un array de bytes
                                byte[] imagenBytes = (byte[])reader["Foto"];

                                // Usamos un MemoryStream para leer los bytes de la imagen
                                using (MemoryStream ms = new MemoryStream(imagenBytes))
                                {
                                    imagUser = new BitmapImage();
                                    imagUser.BeginInit();
                                    imagUser.CacheOption = BitmapCacheOption.OnLoad;
                                    imagUser.StreamSource = ms;
                                    imagUser.EndInit();
                                }
                            }

                            else
                            {
                                imagUser = null;
                            }

                            // Creamos un nuevo objeto tipo UsuarioModel y lo agregamos a la lista
                            usuario = new UsuarioModel(idUsuario, nombre, apellidos, email, username,
                                contrasenia, imagUser, rol, departamento);
                            
                            _usuarioList.Add(usuario);
                        }

                    }

                }
                return _usuarioList;
            }
        }


        // MÉTODO Agregar Usuario a la Base de Datos
        public void AddUsuario(UsuarioModel usuarioModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta SQL Insertar Usuario
                string query = @"INSERT INTO Usuarios (Nombre, Apellidos, Email, Username, Contrasenia, Foto, Rol, Departamento) 
                                VALUES (@Nombre, @Apellidos, @Email, @Username, @Contrasenia, @Foto, @Rol, @Departamento);";

                // Ejecutamos la Consulta
                using (SqlCommand cmdQuery = new SqlCommand(query, connection))
                {
                    cmdQuery.Parameters.AddWithValue("@Nombre", usuarioModel.Nombre);
                    cmdQuery.Parameters.AddWithValue("@Apellidos", usuarioModel.Apellidos);
                    cmdQuery.Parameters.AddWithValue("@Email", usuarioModel.Email);
                    cmdQuery.Parameters.AddWithValue("@Username", usuarioModel.Username);
                    cmdQuery.Parameters.AddWithValue("@Contrasenia", usuarioModel.Contrasenia);
                    cmdQuery.Parameters.AddWithValue("@Rol", usuarioModel.Rol);
                    cmdQuery.Parameters.AddWithValue("@Departamento", usuarioModel.Departamento);

                    // Manejo de la imagen
                    if (usuarioModel.Foto != null)
                    {
                        // Convertir la imagen a un array de bytes
                        byte[] imagenBytes;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(usuarioModel.Foto));
                            encoder.Save(ms);
                            imagenBytes = ms.ToArray();
                        }

                        cmdQuery.Parameters.AddWithValue("@Foto", imagenBytes);
                    }
                    else
                    {
                        // Si no hay imagen, dejamos null por defecto
                        cmdQuery.Parameters.AddWithValue("@Foto", DBNull.Value);
                    }

                    // Ejecutamos la Inserción de los Datos
                    cmdQuery.ExecuteNonQuery();
                }
            }
        }


        // MÉTODO Eliminar Usuario
        public void RemoveUsuario(UsuarioModel usuarioModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta SQL
                string query = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario;";

                // Ejecutamos la Consulta
                using (SqlCommand cmdQuery = new SqlCommand(query, connection))
                {
                    cmdQuery.Parameters.AddWithValue("@IdUsuario", usuarioModel.IdUsuario);
                    cmdQuery.ExecuteNonQuery();
                }
            }
        }

        // ACTUALIZAR USUARIO
        public void UpdateUsuario(UsuarioModel updatedUsuario)
        {
            // Buscar por ID en la lista
            /*
            var existing = _usuarioList.FirstOrDefault(u => u.IdUsuario == updatedUsuario.IdUsuario);
            if (existing != null)
            {
                existing.Nombre = updatedUsuario.Nombre;
                existing.Apellidos = updatedUsuario.Apellidos;
                existing.Email = updatedUsuario.Email;
                existing.Username = updatedUsuario.Username;
                existing.Contrasenia = updatedUsuario.Contrasenia;
                existing.Foto = updatedUsuario.Foto;
                existing.Rol = updatedUsuario.Rol;
                existing.Departamento = updatedUsuario.Departamento;
            }
            */
        }
    }
}
