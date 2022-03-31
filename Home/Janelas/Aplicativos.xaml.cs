using Home;
using Home.Janelas;
using Infraestrutura.Entidades;
using Infraestrutura.Enumeradores;
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

namespace BrielinaUtilitarios.Janelas
{
    /// <summary>
    /// Interação lógica para Aplicativos.xam
    /// </summary>
    public partial class Aplicativos : Page
    {
        public Aplicativos(Usuario userLogado, MainWindow JanelaPrincipal)
        {
            InitializeComponent();
            FuncionalidadeAnimes.Visibility = Visibility.Collapsed;
            FuncionalidadeFinanceiro.Visibility = Visibility.Collapsed;
            FuncionalidadeSteam.Visibility = Visibility.Collapsed;

            foreach (var funcionalidadeUsuario in userLogado.FuncionalidadesPermitidas.Split(';'))
            {
                switch (funcionalidadeUsuario)
                {
                    case "0":
                        var a = Funcionalidades.Animes;
                        FuncionalidadeAnimes.Visibility = Visibility.Visible;
                        break;
                    case "1":
                        var b = Funcionalidades.Financeiro;
                        FuncionalidadeFinanceiro.Visibility = Visibility.Visible;
                        break;
                    case "2":
                        var c = Funcionalidades.Steam;
                        FuncionalidadeSteam.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void AnimesApp(object sender, RoutedEventArgs e)
        {
            Inicio AnimeInicio = new Inicio();

            this.NavigationService.Navigate(AnimeInicio);
        }

        private void FinanceiroApp(object sender, RoutedEventArgs e)
        {
            Financeiro.inicio FinanceiroInicio = new Financeiro.inicio();

            this.NavigationService.Navigate(FinanceiroInicio);
        }

        private void SteamJogosApp(object sender, RoutedEventArgs e)
        {
            SteamJogos.Inicio SteamJogosInicio = new SteamJogos.Inicio();

            this.NavigationService.Navigate(SteamJogosInicio);
        }
    }
}
