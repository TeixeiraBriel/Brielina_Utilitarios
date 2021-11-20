using BrielinaUtilitarios.Janelas;
using Home.Controladores;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Home
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private ControladorAnimes _controlador = new ControladorAnimes();

        public MainWindow()
        {
            InitializeComponent();

            if (nomeUsuario.Content.ToString() == "Usuario")
            {
                nomeUsuario.Content = "Usuario: Geral";
            }
            txtVersao.Content = System.Windows.Forms.Application.ProductVersion;
            Aplicativos inicio = new Aplicativos();
            this.janelaPrincipal.NavigationService.Navigate(inicio);
            verificaApi();
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

        private void clicaStatusApi(object sender, MouseButtonEventArgs e)
        {
            verificaApi();
        }

        private void NavegarInicio(object sender, RoutedEventArgs e)
        {
            Aplicativos inicio = new Aplicativos();

            this.janelaPrincipal.NavigationService.Navigate(inicio);
        }

        public void verificaApi()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                try
                {
                    var testeApi = _controlador.buscarAnimes();

                    if (testeApi != null)
                    {
                        statusApiLabel.Content = "Status API: Online";
                        statusApiLabel.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        statusApiLabel.Content = "Status API: Offline";
                        statusApiLabel.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
                catch
                {
                    statusApiLabel.Content = "Status API: Offline";
                    statusApiLabel.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }
    }
}
