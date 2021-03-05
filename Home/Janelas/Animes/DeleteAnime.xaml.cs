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

namespace Home.Janelas.Animes
{
    /// <summary>
    /// Interação lógica para DeleteAnime.xam
    /// </summary>
    public partial class DeleteAnime : Page
    {
        private Page JanelaAnterior;
        private Controlador _controlador;
        private int Id;
        private Anime animeApagar;

        public DeleteAnime(Page janelaAnterior, int id)
        {
            InitializeComponent();
            _controlador = new Controlador();
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
