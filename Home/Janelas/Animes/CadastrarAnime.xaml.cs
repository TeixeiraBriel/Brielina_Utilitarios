using Home.Controladores;
using Infraestrutura.Entidades;
using System.Windows;
using System.Windows.Controls;

namespace Home.Janelas.Animes
{
    /// <summary>
    /// Interação lógica para CadastrarAnime.xam
    /// </summary>
    public partial class CadastrarAnime : Page
    {
        private MainWindow JanelaPrincipal;
        private Page Anterior;

        public CadastrarAnime(MainWindow janelaPrincipal = null, Page anterior = null)
        {
            InitializeComponent();
            JanelaPrincipal = janelaPrincipal;
            Anterior = anterior;
        }

        private void Voltar(object sender, RoutedEventArgs e)
        {
            if (Anterior != null)
            {
            this.NavigationService.Navigate(Anterior);
            }
            else
            {
                var destino = new Inicio(JanelaPrincipal);
                this.NavigationService.Navigate(destino);
            }
        }

        private void cadastrarAnime(object sender, RoutedEventArgs e)
        {
            ControladorAnimes x = new ControladorAnimes();

            Anime novoAnime = new Anime();
            novoAnime.Nome = inputNome.Text;
            novoAnime.Episodios = inputEpisodios.Text == "" ? 0 : int.Parse(inputEpisodios.Text);
            novoAnime.Generos = inputGeneros.Text;
            novoAnime.Completo = inputCompleto.Text;
            novoAnime.Link = inputLink.Text;
            novoAnime.LinkImage = inputLinkImage.Text;
            novoAnime.DiaLancamento = inputDiaSemana.Text;

            x.cadastroAnimes(novoAnime);

            inputNome.Text = null;
            inputEpisodios.Text = null;
            inputGeneros.Text = null;
            inputCompleto.Text = null;
            inputLink.Text = null;
            inputLinkImage.Text = null;
            inputDiaSemana.Text = null;

            TabelaAnimes anterior = new TabelaAnimes();

            this.NavigationService.Navigate(anterior);
        }
    }
}
