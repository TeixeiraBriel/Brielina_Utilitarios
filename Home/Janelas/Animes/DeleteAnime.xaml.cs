using Home.Controladores;
using Infraestrutura.Entidades;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Home.Janelas.Animes
{
    /// <summary>
    /// Interação lógica para DeleteAnime.xam
    /// </summary>
    public partial class DeleteAnime : Page
    {
        private Page JanelaAnterior;
        private ControladorAnimes _controlador;
        private int Id;
        private Anime animeApagar;

        public DeleteAnime(Page janelaAnterior, int id)
        {
            InitializeComponent();
            _controlador = new ControladorAnimes();
            JanelaAnterior = janelaAnterior;
            carregarAnime(id);
        }

        private void carregarAnime(int id)
        {
            Id = id;

            animeApagar = _controlador.buscarAnime(id);
            if (!string.IsNullOrEmpty(animeApagar.LinkImage))
            {
                var img = new ImageSourceConverter().ConvertFromString(animeApagar.LinkImage) as ImageSource;
                imgAnime.Source = img;
            }

            inputName.Text = animeApagar.Nome;
            inputEpisodios.Text = animeApagar.Episodios.ToString(); 
            inputGeneros.Text = animeApagar.Generos;
            inputCompleto.Text = animeApagar.Completo;
            inputLink.Text = animeApagar.Link;
            inputDiaLançamento.Text = animeApagar.DiaLancamento;
        }

        private void Voltar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(JanelaAnterior);
        }

        private void DeletarAnime(object sender, RoutedEventArgs e)
        {
            _controlador.deleteAnime(Id);
            TabelaAnimes anterior = new TabelaAnimes();

            this.NavigationService.Navigate(anterior);
        }
    }
}
