using Home.Janelas.Animes;
using System.Windows;
using System.Windows.Controls;

namespace Home.Janelas
{
    /// <summary>
    /// Interação lógica para Inicio.xam
    /// </summary>
    public partial class Inicio : Page
    {
        private MainWindow JanelaPrincipal;

        public Inicio(MainWindow janelaPrincipal = null)
        {
            InitializeComponent();
            JanelaPrincipal = janelaPrincipal;

        }

        private void navegaCadastro(object sender, RoutedEventArgs e)
        {
            var destino = new CadastrarAnime(JanelaPrincipal);

            this.NavigationService.Navigate(destino);
        }

        private void navegaTabelas(object sender, RoutedEventArgs e)
        {
            var destino = new TabelaAnimes(JanelaPrincipal);

            this.NavigationService.Navigate(destino);
        }
    }
}
