using BrielinaFinanceiro.Entidades;
using BrielinaUtilitarios.Controladores;
using Estrutura.Entidades;
using Home;
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

namespace BrielinaUtilitarios.Janelas.Login
{
    /// <summary>
    /// Interação lógica para inicio.xam
    /// </summary>
    public partial class inicio : Page
    {
        private MainWindow JanelaPrincipal;

        public inicio(MainWindow janelaPrincipal = null)
        {
            InitializeComponent();
            JanelaPrincipal = janelaPrincipal;
            janelaPrincipal.buttonAplicacoes.Visibility = Visibility.Collapsed;
        }

        private void ValidaLogin(object sender, RoutedEventArgs e)
        {
            if (inputLogin.Text == inputSenha.Password)
            {
                JanelaPrincipal.buttonAplicacoes.Visibility = Visibility.Visible;
                Aplicativos inicio = new Aplicativos();
                JanelaPrincipal.janelaPrincipal.NavigationService.Navigate(inicio);
                JanelaPrincipal.nomeUsuario.Content = inputLogin.Text;
            }

        }

        private void NavegaCriarConta(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
