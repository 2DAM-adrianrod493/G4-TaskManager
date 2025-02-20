using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using G4_EmployeeRegister.Models;
using Microsoft.Data.SqlClient;

namespace G4_EmployeeRegister.Services
{
    public class UsuarioService
    {
        // CREAMOS UNA LISTA PRIVADA
        private ObservableCollection<UsuarioModel> _usuarioList { get; set; }
        private string connectionString = ConfigurationManager.ConnectionStrings["Conexion_App"].ConnectionString;


        // OBTENEMOS LOS USUARIOS
        public ObservableCollection<UsuarioModel> GetAllUsuarios()
        {
            _usuarioList = new ObservableCollection<UsuarioModel>();
            UsuarioModel usuario = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT IdUsuario, Nombre, Apellidos, Email, Username, 
                            Contrasenia,Foto, Rol,
                            Departamento FROM Usuarios Where Rol='Usuario';
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
                            
                            // Manejo de la imagen
                            BitmapImage imagUser = null; // Inicializa la imagen como null

                            if (reader["Foto"] != DBNull.Value) // Verifica si no es NULL
                            {
                                // Convertir el resultado a un array de bytes
                                byte[] imagenBytes = (byte[])reader["Foto"];

                                // Utilizar un MemoryStream para leer los bytes de la imagen
                                using (MemoryStream ms = new MemoryStream(imagenBytes))
                                {
                                    imagUser = new BitmapImage();
                                    imagUser.BeginInit();
                                    imagUser.CacheOption = BitmapCacheOption.OnLoad; // Cargar la imagen completamente en memoria
                                    imagUser.StreamSource = ms; // Asignar el MemoryStream como fuente de la imagen
                                    imagUser.EndInit();
                                }
                            }

                            else
                            {
                                imagUser = null;
                            }

                            usuario = new UsuarioModel(idUsuario, nombre, apellidos, email, username,
                                contrasenia, imagUser, rol, departamento);
                            _usuarioList.Add(usuario);
                        }

                    }

                }
                return _usuarioList;
            }
        }


        // AGREGAR USUARIO
        public void AddUsuario(UsuarioModel usuarioModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO Usuarios (Nombre, Apellidos, Email, Username, Contrasenia, Foto, Rol, Departamento) 
                         VALUES (@Nombre, @Apellidos, @Email, @Username, @Contrasenia, @Foto, @Rol, @Departamento);";

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
                        // Si no hay imagen, insertar NULL
                        cmdQuery.Parameters.AddWithValue("@Foto", DBNull.Value);
                    }

                    cmdQuery.ExecuteNonQuery();
                }
            }
        }


        // ELIMINAR USUARIO
        public void RemoveUsuario(UsuarioModel usuarioModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario;";

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Verifica si el usuario existe antes de actualizar
                string checkQuery = "SELECT COUNT(*) FROM Usuarios WHERE IdUsuario = @IdUsuario";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@IdUsuario", updatedUsuario.IdUsuario);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Usuario no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                // Si el usuario existe, procede con la actualización
                string updateQuery = @"UPDATE Usuarios SET 
                                Nombre = @Nombre, Apellidos = @Apellidos, Email = @Email, 
                                Username = @Username, Contrasenia = @Contrasenia, 
                                Foto = @Foto, Rol = @Rol, Departamento = @Departamento
                                WHERE IdUsuario = @IdUsuario";

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                {
                    updateCmd.Parameters.AddWithValue("@IdUsuario", updatedUsuario.IdUsuario);
                    updateCmd.Parameters.AddWithValue("@Nombre", updatedUsuario.Nombre);
                    updateCmd.Parameters.AddWithValue("@Apellidos", updatedUsuario.Apellidos);
                    updateCmd.Parameters.AddWithValue("@Email", updatedUsuario.Email);
                    updateCmd.Parameters.AddWithValue("@Username", updatedUsuario.Username);
                    updateCmd.Parameters.AddWithValue("@Contrasenia", updatedUsuario.Contrasenia);
                    updateCmd.Parameters.AddWithValue("@Rol", updatedUsuario.Rol);
                    updateCmd.Parameters.AddWithValue("@Departamento", updatedUsuario.Departamento);

                    // Manejo de imagen
                    if (updatedUsuario.Foto != null)
                    {
                        byte[] imagenBytes;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(updatedUsuario.Foto));
                            encoder.Save(ms);
                            imagenBytes = ms.ToArray();
                        }
                        updateCmd.Parameters.AddWithValue("@Foto", imagenBytes);
                    }
                    else
                    {
                        updateCmd.Parameters.AddWithValue("@Foto", DBNull.Value);
                    }

                    updateCmd.ExecuteNonQuery();
                    MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

    }
}
