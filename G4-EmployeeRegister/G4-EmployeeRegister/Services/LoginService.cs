using G4_EmployeeRegister.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace G4_EmployeeRegister.Services
{
    internal class LoginService
    {
        // Cadena de Conexión Base de Datos
        private string connectionString = ConfigurationManager.ConnectionStrings["Conexion_App"].ConnectionString;
        
        // MÉTODO Obtener credenciales del Usuario
        public UsuarioModel GetUsuarioLogin(string nombreUsuario, string passIntro)
        {
            UsuarioModel usuario = null;
            try
            {
                // Conexión
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    // Consulta SQL
                    string query = @"SELECT u.IdUsuario, u.Nombre, u.Apellidos, u.Email, u.Username, u.Contrasenia, u.Foto, u.Rol, u.Departamento 
                                     FROM Usuarios u
                                     WHERE Username = @userName";

                    // Ejecutamos la Consulta
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@userName", nombreUsuario);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string passHash = reader["Contrasenia"].ToString();
                                if (BCrypt.Net.BCrypt.Verify(passIntro, passHash))

                                {
                                    int idUsuario = Convert.ToInt32(reader["IdUsuario"]);
                                    string nombre = reader["Nombre"].ToString();
                                    string apellidos = reader["Apellidos"].ToString();
                                    string email = reader["Email"].ToString();
                                    string username = reader["Username"].ToString();
                                    string contrasenia = passHash;
                                    string rol = reader["Rol"].ToString();
                                    string departamento = reader["Departamento"].ToString();
                                    BitmapImage foto = new BitmapImage();



                                    // Se inicializa FichajeModel en null por ahora
                                    usuario = new UsuarioModel(idUsuario, nombre, apellidos, email, username, contrasenia, null, rol, departamento);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el usuario: " + ex.Message);
            }
            return usuario;
        }
    }
}
