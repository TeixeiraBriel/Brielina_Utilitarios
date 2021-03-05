using Home.Janelas;
using System.Windows;

namespace Home
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var inicio = new Inicio(this);

            this.janelaPrincipal.NavigationService.Navigate(inicio);
        }
    }
}
