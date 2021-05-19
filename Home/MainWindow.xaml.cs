using Home.Janelas;
using System.Windows;
using System.Windows.Input;

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

        private void FecharPrograma(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MoverTela(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
