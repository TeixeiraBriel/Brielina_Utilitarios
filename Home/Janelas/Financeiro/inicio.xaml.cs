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
    /// Interação lógica para inicio.xam
    /// </summary>
    public partial class inicio : Page
    {
        public inicio()
        {
            InitializeComponent();
        }

        private void navegaCadastroGastos(object sender, RoutedEventArgs e)
        {
            var destino = new CadastroDeGasto();

            this.NavigationService.Navigate(destino);
        }

        private void navegaCadastroEntrada(object sender, RoutedEventArgs e)
        {
            var destino = new CadastroEntrada();

            this.NavigationService.Navigate(destino);
        }

        private void navegaTabelaEntrada(object sender, RoutedEventArgs e)
        {
            var destino = new TabelaEntrada();

            this.NavigationService.Navigate(destino);
        }

        private void navegaTabelaSaida(object sender, RoutedEventArgs e)
        {
            var destino = new TabelaGastos();

            this.NavigationService.Navigate(destino);
        }
    }
}
