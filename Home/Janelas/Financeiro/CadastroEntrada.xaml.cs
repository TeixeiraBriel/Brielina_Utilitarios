using BrielinaFinanceiro.Entidades;
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

namespace BrielinaUtilitarios.Janelas.Financeiro
{
    /// <summary>
    /// Interação lógica para CadastroEntrada.xam
    /// </summary>
    public partial class CadastroEntrada : Page
    {
        private bool firstA = true;
        private bool firstB = true;
        private bool firstC = true;
        private bool firstD = true;
        Controladores.ControladorFinanceiro _controlador;

        public CadastroEntrada()
        {
            InitializeComponent();
            _controlador = new Controladores.ControladorFinanceiro();
        }

        private void DataDiferenteFunc(object sender, RoutedEventArgs e)
        {
            if (firstA)
            {
                firstA = false;
                return;
            }
            if (DataDiferenteRadio.IsChecked == true)
            {
                DataInseridoPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                DataInseridoPanel.Visibility = Visibility.Visible;
            }
        }

        private void CadastrarEntrada(object sender, RoutedEventArgs e)
        {
            Registro novoRegistro = new Registro();
            try
            {
                Aviso.Visibility = Visibility.Collapsed;
                novoRegistro.Descricao = txtDescricao.Text;
                novoRegistro.Valor = Double.Parse(txtValor.Text);
                if (DataDiferenteRadio.IsChecked == true)
                {
                    novoRegistro.Data = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else
                {
                    novoRegistro.Data = txtDataInserido.Text;
                }
                novoRegistro.Grupo = txtGrupo.Text;
                novoRegistro.Credito =  0;
                novoRegistro.Fixa =  0;
                novoRegistro.DataVencimento = "";
                novoRegistro.Tipo = 0;

                if (string.IsNullOrEmpty(novoRegistro.Descricao) || novoRegistro.Valor == null)
                {
                    throw new Exception();
                }

                _controlador.cadastroRegistro(novoRegistro);

                Aviso.Visibility = Visibility.Visible;
                Aviso.Foreground = Brushes.Green;
                Aviso.Text = "Registro cadastrado com sucesso";

                var destino = new TabelaEntrada();
                this.NavigationService.Navigate(destino);
            }
            catch
            {
                Aviso.Visibility = Visibility.Visible;
                Aviso.Foreground = Brushes.Red;
                Aviso.Text = "Não foi possivel realizar o cadastro, favor validar os campos.";
            }
        }

        private void funcVoltar(object sender, RoutedEventArgs e)
        {
            var destino = new inicio();

            this.NavigationService.Navigate(destino);
        }
    }
}
