using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace G4_EmployeeRegister.ViewModels
{
    // RelayCommand es una Clase Auxiliar para ICommand
    // ICommand: esta interfaz (propia de WPF) define la estructura para crear comandos
    // (con Execute, CanExecute, y el evento CanExecuteChanged)
    public class RelayCommand : ICommand
    {
        // Campos Privados para Almacenar las Funciones
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        // Constructor que Recibe la Acción que se va a Ejecutar y
        // Opcionalmente la Función para Verificar si puede Ejecutarse (para que se active o no)
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // Implementación de CanExecute (ICommand)
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        // Implementación de Execute (ICommand)
        public void Execute(object parameter) => _execute(parameter);

        // Evento CanExecuteChanged (ICommand)
        // WPF llama a este evento para "saber" cuándo tiene que volver a "revisar" CanExecute
        // (por ejemplo, para habilitar o deshabilitar un botón).
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
