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

namespace BrielinaUtilitarios.Janelas.ControladorFinanceiro
{
    /// <summary>
    /// Interação lógica para inicio.xam
    /// </summary>
    public partial class inicio : Page
    {
        public inicio()
        {
            InitializeComponent();
        }

        private void NavegaDadosView(object sender, RoutedEventArgs e)
        {
            dadosFinanceiroView View = new dadosFinanceiroView();
            framePrincipalFinanceiro.NavigationService.Navigate(View);
        }

        private void NavegaEntradaView(object sender, RoutedEventArgs e)
        {
            entradaFinanceiroView View = new entradaFinanceiroView();
            framePrincipalFinanceiro.NavigationService.Navigate(View);
        }

        private void NavegaSaidaView(object sender, RoutedEventArgs e)
        {
            saidaFinanceiroView View = new saidaFinanceiroView();
            framePrincipalFinanceiro.NavigationService.Navigate(View);
        }

        private void NavegaContasView(object sender, RoutedEventArgs e)
        {
            contasFinanceiroView View = new contasFinanceiroView();
            framePrincipalFinanceiro.NavigationService.Navigate(View);
        }
    }
}
