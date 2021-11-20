using Home.Controladores;
using Infraestrutura.Entidades;
using System.Windows;
using System.Windows.Controls;

namespace Home.Janelas.Animes
{
    /// <summary>
    /// Interação lógica para EditAnime.xam
    /// </summary>
    public partial class EditAnime : Page
    {
        private Page JanelaAnterior;
        private ControladorAnimes _controlador;
        private int Id;
        private Anime animeEditar;

        public EditAnime(Page janelaAnterior, int id)
        {
            InitializeComponent();
            _controlador = new ControladorAnimes();
            JanelaAnterior = janelaAnterior;
            Id = id;
            carregarAnime(id);
        }

        private void carregarAnime(int id)
        {
            Id = id;
            animeEditar = _controlador.buscarAnime(id);

            inputNome.Text = animeEditar.Nome;
            inputEpisodios.Text = animeEditar.Episodios.ToString();
            inputGeneros.Text = animeEditar.Generos;
            inputCompleto.Text = animeEditar.Completo;
            inputLink.Text = animeEditar.Link;
            inputLinkImage.Text = animeEditar.LinkImage;
            inputDiaSemana.Text = animeEditar.DiaLancamento;
            if (animeEditar.Finalizada == 0)
            {
                radioFinalizadoTrue.IsChecked = false;
                radioFinalizadoFalse.IsChecked = true;                
            }
            else
            {
                radioFinalizadoTrue.IsChecked = true;
                radioFinalizadoFalse.IsChecked = false;
            }

        }
        private void Voltar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(JanelaAnterior);
        }

        private void EditarAnime(object sender, RoutedEventArgs e)
        {
            ControladorAnimes x = new ControladorAnimes();

            animeEditar.Episodios = inputEpisodios.Text == "" ? 0 : int.Parse(inputEpisodios.Text);
            animeEditar.Generos = inputGeneros.Text;
            animeEditar.Completo = inputCompleto.Text;
            animeEditar.Link = inputLink.Text;
            animeEditar.LinkImage = inputLinkImage.Text;
            animeEditar.DiaLancamento = inputDiaSemana.Text;
            animeEditar.Finalizada = radioFinalizadoTrue.IsChecked == true ? 1 : 0;

            x.EditarAnime(animeEditar);

            TabelaAnimes anterior = new TabelaAnimes();

            this.NavigationService.Navigate(anterior);
        }
    }
}
