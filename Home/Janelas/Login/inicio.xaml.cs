using BrielinaFinanceiro.Entidades;
using BrielinaUtilitarios.Controladores;
using Estrutura.Entidades;
using Home;
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

namespace BrielinaUtilitarios.Janelas.Login
{
    /// <summary>
    /// Interação lógica para inicio.xam
    /// </summary>
    public partial class inicio : Page
    {
        private MainWindow JanelaPrincipal;
        private ControladorLogin _controlador;

        public inicio(MainWindow janelaPrincipal = null)
        {
            InitializeComponent();
            JanelaPrincipal = janelaPrincipal;
            janelaPrincipal.buttonAplicacoes.Visibility = Visibility.Collapsed;
            _controlador = new ControladorLogin();
        }

        private void ValidaLogin(object sender, RoutedEventArgs e)
        {
            Logar();
        }

        private void NavegaCriarConta(object sender, MouseButtonEventArgs e)
        {

        }

        private void entrarConvidado(object sender, RoutedEventArgs e)
        {
            inputLogin.Text = "convidado";
            inputSenha.Password = "convidado";

            Logar();
        }

        public void Logar()
        {
            Usuario UsuarioValido = _controlador.usuarioValido(new Usuario { user = inputLogin.Text, pass = inputSenha.Password });

            if (UsuarioValido != null)
            {
                JanelaPrincipal.buttonAplicacoes.Visibility = Visibility.Visible;
                Aplicativos inicio = new Aplicativos(UsuarioValido, JanelaPrincipal);
                JanelaPrincipal.janelaPrincipal.NavigationService.Navigate(inicio);
                JanelaPrincipal.nomeUsuario.Content = UsuarioValido.nickname;
                JanelaPrincipal.controladorGeral._usuarioLogado = UsuarioValido;
            }
        }
    }
}
