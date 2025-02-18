using G4_EmployeeRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace G4_EmployeeRegister.Views
{
    /// <summary>
    /// Lógica de interacción para VerFichajesPage.xaml
    /// </summary>
    public partial class VerFichajesPage : Page
    {
        public VerFichajesPage(UsuarioModel usuario)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.VerFichajesViewModel(usuario);
            
        }
    }
}
