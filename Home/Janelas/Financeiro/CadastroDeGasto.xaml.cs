using BrielinaFinanceiro.Entidades;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BrielinaUtilitarios.Janelas.Financeiro
{
    /// <summary>
    /// Interação lógica para CadastroDeGasto.xam
    /// </summary>
    public partial class CadastroDeGasto : Page
    {
        private bool firstA = true;
        private bool firstB = true;
        private bool firstC = true;
        private bool firstD = true;
        Controladores.ControladorFinanceiro _controlador;

        public CadastroDeGasto()
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

        private void GastoFixoRadioFunc(object sender, RoutedEventArgs e)
        {
            if (firstB)
            {
                firstB = false;
                return;
            }
            if (GastoFixoRadio.IsChecked == true)
            {
                gastoFixoPanel.Visibility = Visibility.Visible;
                DataVencimentoPanel.Visibility = Visibility.Visible;
            }
            else
            {
                gastoFixoPanel.Visibility = Visibility.Collapsed;
                DataVencimentoPanel.Visibility = Visibility.Collapsed;
            }

        }

        private void CadastrarGasto(object sender, RoutedEventArgs e)
        {
            Registro novoRegistro = new Registro();
            if (true)
            {

            }
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
                novoRegistro.Credito = cartaoCreditoRadio.IsChecked == true ? 1 : 0;
                novoRegistro.Fixa = GastoFixoRadio.IsChecked == true ? 1 : 0;
                novoRegistro.DataVencimento = txtDataVencimento.Text;
                novoRegistro.Tipo = 1;

                if (string.IsNullOrEmpty(novoRegistro.Descricao) || novoRegistro.Valor == null)
                {
                    throw new Exception();
                }

                _controlador.cadastroRegistro(novoRegistro);

                Aviso.Visibility = Visibility.Visible;
                Aviso.Foreground = Brushes.Green;
                Aviso.Text = "Registro cadastrado com sucesso";

                var destino = new TabelaGastos();
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
