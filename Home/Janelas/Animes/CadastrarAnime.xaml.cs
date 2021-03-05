﻿using Animes;
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

        public CadastrarAnime(MainWindow janelaPrincipal)
        {
            InitializeComponent();
            JanelaPrincipal = janelaPrincipal;
        }

        private void Voltar(object sender, RoutedEventArgs e)
        {
            var destino = new Inicio(JanelaPrincipal);

            this.NavigationService.Navigate(destino);
        }

        private void cadastrarAnime(object sender, RoutedEventArgs e)
        {
            Controlador x = new Controlador();

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
