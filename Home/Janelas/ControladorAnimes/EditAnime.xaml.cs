using Animes;
using Infraestrutura.Entidades;
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

namespace Janelas.ControladorAnimes
{
    /// <summary>
    /// Interação lógica para EditAnime.xam
    /// </summary>
    public partial class EditAnime : Page
    {
        private Page JanelaAnterior;
        private Controlador _controlador;
        private int Id;
        private Anime animeEditar;

        public EditAnime(Page janelaAnterior, int id)
        {
            InitializeComponent();
            _controlador = new Controlador();
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
        }
        private void Voltar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(JanelaAnterior);
        }

        private void EditarAnime(object sender, RoutedEventArgs e)
        {
            Controlador x = new Controlador();

            animeEditar.Episodios = inputEpisodios.Text == "" ? 0 : int.Parse(inputEpisodios.Text);
            animeEditar.Generos = inputGeneros.Text;
            animeEditar.Completo = inputCompleto.Text;
            animeEditar.Link = inputLink.Text;
            animeEditar.LinkImage = inputLinkImage.Text;
            animeEditar.DiaLancamento = inputDiaSemana.Text;

            x.EditarAnime(animeEditar);

            TabelaAnimes anterior = new TabelaAnimes();

            this.NavigationService.Navigate(anterior);
        }
    }
}
