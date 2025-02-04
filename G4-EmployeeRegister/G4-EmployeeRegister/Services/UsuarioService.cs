using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using G4_EmployeeRegister.Models;

namespace G4_EmployeeRegister.Services
{
    public class UsuarioService
    {
        // Creamos Lista Privada
        private ObservableCollection<UsuarioModel> _usuarioList { get; set; }

        // Obtener Listado de Usuarios ya Definidos
        public ObservableCollection<UsuarioModel> GetAllUsuarios()
        {
           FichajeModel f1 = new FichajeModel(1,1,DateTime.Now,"hola","hecha");
            _usuarioList = new ObservableCollection<UsuarioModel>();

            _usuarioList.Add(new UsuarioModel(1, "Adrián", "Rodríguez Rodríguez", "adrianrodry55@gmail.com", "rodry", "adri555", "adri.jpg", "Empleado", "Informática", f1));
            _usuarioList.Add(new UsuarioModel(2, "Irene Naya", "Sánchez Bolívar", "laura.gomez@mail.com", "laurag", "naya555", "naya.jpg", "Empleado", "Logística", f1));
            _usuarioList.Add(new UsuarioModel(3, "Carlos Pérez", "González", "carlos.perez@mail.com", "carlosp", "perez123", "carlos.jpg", "Empleado", "Ventas", f1));
            _usuarioList.Add(new UsuarioModel(4, "Ana Ruiz", "Martínez", "ana.ruiz@mail.com", "anar", "ruiz456", "ana.jpg", "Empleado", "Marketing", f1));
            _usuarioList.Add(new UsuarioModel(5, "David López", "Fernández", "david.lopez@mail.com", "davidl", "lopez789", "david.jpg", "Empleado", "Recursos Humanos", f1));
            _usuarioList.Add(new UsuarioModel(6, "María González", "Rodríguez", "maria.gonzalez@mail.com", "mariag", "gonzalez321", "maria.jpg", "Empleado", "Finanzas", f1));
            _usuarioList.Add(new UsuarioModel(7, "Juan Torres", "Sánchez", "juan.torres@mail.com", "juant", "torres654", "juan.jpg", "Empleado", "Tecnología", f1));
            _usuarioList.Add(new UsuarioModel(8, "Lucía Martín", "Pérez", "lucia.martin@mail.com", "luciam", "martin987", "lucia.jpg", "Empleado", "Atención al Cliente", f1));
            _usuarioList.Add(new UsuarioModel(9, "Pedro Gómez", "López", "pedro.gomez@mail.com", "pedrog", "gomez123", "pedro.jpg", "Empleado", "Logística", f1));
            _usuarioList.Add(new UsuarioModel(10, "Sandra Díaz", "Vázquez", "sandra.diaz@mail.com", "sandrad", "diaz456", "sandra.jpg", "Empleado", "Marketing", f1));
            _usuarioList.Add(new UsuarioModel(11, "Raúl Martín", "Jiménez", "raul.martin@mail.com", "raulm", "martin789", "raul.jpg", "Empleado", "Recursos Humanos", f1));
            _usuarioList.Add(new UsuarioModel(12, "Esther Rodríguez", "Sánchez", "esther.rodriguez@mail.com", "estherr", "rodriguez321", "esther.jpg", "Empleado", "Ventas", f1));
            _usuarioList.Add(new UsuarioModel(13, "Víctor Torres", "Pérez", "victor.torres@mail.com", "victort", "torres654", "victor.jpg", "Empleado", "Tecnología", f1));
            _usuarioList.Add(new UsuarioModel(14, "Elena Fernández", "González", "elena.fernandez@mail.com", "elenaf", "fernandez987", "elena.jpg", "Empleado", "Atención al Cliente", f1));
            _usuarioList.Add(new UsuarioModel(15, "Tomás Sánchez", "Martínez", "tomas.sanchez@mail.com", "tomass", "sanchez123", "tomas.jpg", "Empleado", "Informática", f1));
            _usuarioList.Add(new UsuarioModel(16, "Marta López", "Vázquez", "marta.lopez@mail.com", "martal", "lopez456", "marta.jpg", "Empleado", "Finanzas", f1));

            return _usuarioList;
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
            var existing = _usuarioList.FirstOrDefault(u => u.IdUsuario == updatedUsuario.IdUsuario);
            if (existing != null)
            {
                existing.Nombre = updatedUsuario.Nombre;
                existing.Apellidos = updatedUsuario.Apellidos;
                existing.Email = updatedUsuario.Email;
                existing.Username = updatedUsuario.Username;
                existing.Contraseña = updatedUsuario.Contraseña;
                existing.Foto = updatedUsuario.Foto;
                existing.Rol = updatedUsuario.Rol;
                existing.Departamento = updatedUsuario.Departamento;
            }
        }
    }
}
