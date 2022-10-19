using BrielinaUtilitarios.Controladores;
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

namespace BrielinaUtilitarios.Janelas.SteamJogos
{
    /// <summary>
    /// Interação lógica para Inicio.xam
    /// </summary>
    public partial class Inicio : Page
    {
        private int posicao = 1;
        private int totalJogado = 0;
        private bool init = true;

        List<Player> Players;
        ControladorSteamJogos _mineradorController;
        OwnedGames listaJogos;

        public Inicio()
        {
            InitializeComponent();
            _mineradorController = new ControladorSteamJogos();
            Players = new List<Player>();

            Player biel = new Player() { steamid = "76561198124348532", personaname = "BielGt" };
            Player takato = new Player() { steamid = "76561198254468411", personaname = "Takatão" };
            Players.Add(biel);
            Players.Add(takato);
            BoxHistorico.Items.Add(new ComboBoxItem() { Content = biel.personaname });
            BoxHistorico.Items.Add(new ComboBoxItem() { Content = takato.personaname });
        }

        private void RefreshDados(object sender, RoutedEventArgs e)
        {
            buscarPerfil();
        }

        private void buscarPerfil(string steamId = null)
        {
            if (string.IsNullOrEmpty(BoxSteamId.Text) && ItemBoxHistorico1.IsSelected)
            {
                resultadoPesquisa.Foreground = Brushes.Red;
                resultadoPesquisa.Text = $"Preencher Steam Id";
                return;
            }
            listaJogos = _mineradorController.minerarDados(BoxSteamId.Text, RadioJogosGratis.IsChecked);

            if (listaJogos == null)
            {
                BoxSteamId.Text = "Id Invalido!";
                return;
            }

            Player player = ItemHistoricoBusca(BoxSteamId.Text);
            IdentificarSteam.Text = player.personaname;
            AtualizarTela();
        }

        private Player ItemHistoricoBusca(string steamId)
        {
            bool repetido = false;
            OwnedGames perfil = _mineradorController.minerarPerfil(steamId);

            foreach (var player in Players)
            {
                if (perfil.response.players[0].steamid == player.steamid)
                {
                    repetido = true;
                }
            }

            if (!repetido)
            {
                Players.Add(perfil.response.players[0]);
                BoxHistorico.Items.Add(new ComboBoxItem() { Content = perfil.response.players[0].personaname });
            }

            return perfil.response.players[0];
        }

        private void AtualizarTela(string busca = null)
        {
            totalJogado = 0;
            posicao = 1;
            PainelJogos.Children.Clear();

            resultadoPesquisa.Text = $"Buscando...";
            resultadoPesquisa.Foreground = Brushes.Black;

            if (string.IsNullOrEmpty(BoxSteamId.Text))
            {
                resultadoPesquisa.Foreground = Brushes.Red;
                resultadoPesquisa.Text = $"Preencher Steam Id";
                return;
            }

            foreach (var item in listaJogos.response.games.OrderByDescending(i => i.playtime_forever))
            {
                if (string.IsNullOrEmpty(busca))
                {
                    criarLinhas(item.name, converterMinutosHoras(item.playtime_forever), item.img_icon_url, item.appid.ToString());
                    posicao++;
                    totalJogado = totalJogado + item.playtime_forever;
                }
                else if (item.name.ToLower().Contains(busca.ToLower()))
                {
                    criarLinhas(item.name, converterMinutosHoras(item.playtime_forever), item.img_icon_url, item.appid.ToString());
                    posicao++;
                    totalJogado = totalJogado + item.playtime_forever;
                }
            }

            if (listaJogos.response.game_count == 0)
            {
                resultadoPesquisa.Text = $"Nenhum Jogo encontrado.";
            }
            else
            {
                resultadoPesquisa.Text = $"Jogos Encontrados: {listaJogos.response.game_count}";
                horasTotaisJogadas.Text = $"Total Jogado: {converterMinutosHoras(totalJogado)}";
            }
        }

        public string converterMinutosHoras(int minutos)
        {
            if (minutos < 60)
            {
                if (minutos < 1)
                {
                    return $"{minutos} Minutos";
                }
                return $"{minutos} Minutos";
            }

            TimeSpan spWorkMin = TimeSpan.FromMinutes(minutos);
            return string.Format("{0} Horas", (int)spWorkMin.TotalHours);
        }

        public void criarLinhas(string nomePar, string horasPar, string IconCod, string IdGame)
        {
            Grid myGrid = new Grid();

            ColumnDefinition c1 = new ColumnDefinition();
            c1.Width = new GridLength(0, GridUnitType.Auto);

            ColumnDefinition c2 = new ColumnDefinition();
            c2.Width = new GridLength(0, GridUnitType.Auto);

            ColumnDefinition c3 = new ColumnDefinition();
            c3.Width = new GridLength(0, GridUnitType.Auto);

            //add img
            string linkImage = $"https://media.steampowered.com/steamcommunity/public/images/apps/{IdGame}/{IconCod}.jpg";
            var img = new ImageSourceConverter().ConvertFromString(linkImage) as ImageSource;
            Image imgAnime = new Image() {Width = 20, Height = 20, Margin = new Thickness(5) };
            imgAnime.Source = img;
            Grid.SetColumn(imgAnime, 0);

            // add texto
            TextBlock nome = new TextBlock();
            nome.Text = $"{posicao} - {nomePar}";
            nome.FontSize = 20;
            nome.FontWeight = FontWeights.Bold;
            Grid.SetColumn(nome, 1);

            // Add the second text cell to the Grid
            TextBlock horas = new TextBlock();
            horas.Text = horasPar;
            horas.FontSize = 12;
            horas.FontWeight = FontWeights.Bold;
            horas.Margin = new Thickness(10, 0, 0, 0);
            horas.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(horas, 2);

            myGrid.Children.Add(nome);
            myGrid.Children.Add(horas);
            myGrid.Children.Add(imgAnime);

            myGrid.ColumnDefinitions.Add(c1);
            myGrid.ColumnDefinitions.Add(c2);
            myGrid.ColumnDefinitions.Add(c3);

            PainelJogos.Children.Add(myGrid);
        }

        private void VerificaLista(object sender, TextChangedEventArgs e)
        {
            AtualizarTela(BoxJogoEspecifico.Text);
        }

        private void MudarModoPesquisa(object sender, SelectionChangedEventArgs e)
        {
            if (init)
            {
                init = false;
                return;
            }
            if (!ItemBoxHistorico1.IsSelected)
            {
                foreach (var player in Players)
                {
                    if (BoxHistorico.SelectedItem.ToString().Contains(player.personaname))
                    {
                        BoxSteamId.Text = player.steamid;
                    }
                }
                BoxSteamId.IsReadOnly = true;
                buscarPerfil();
                BoxSteamId.Text = "Travado!";
                BoxSteamId.Foreground = Brushes.Red;
                BoxSteamId.Background = Brushes.Black;
            }
            else
            {
                BoxSteamId.IsReadOnly = false;
                BoxSteamId.Text = "";
                BoxSteamId.Foreground = Brushes.Black;
                BoxSteamId.Background = Brushes.Wheat;
            }
        }
    }
}