using BrielinaFinanceiro.Entidades;
using BrielinaUtilitarios.Controladores;
using Estrutura.Entidades;
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
        ControladorFinanceiro _controlador;
        double ValorTotal = 0;

        public inicio()
        {
            InitializeComponent();

            _controlador = new ControladorFinanceiro();

            List<Registro> registros = _controlador.buscarRegistros();

            MediaGastos x = new MediaGastos();
            var Medias = x.criarMedias(registros);

            foreach (var media in Medias)
            {
                DataGridMedias.Items.Add(media);
            }

            foreach (var registro in registros)
            {
                if (registro.Tipo == 1)
                {
                    registro.Valor = registro.Valor * -1;
                }
                DataGridResumoGeral.Items.Add(registro);

                ValorTotal = ValorTotal + registro.Valor;
            }
            DataGridResumoGeral.Items.Add(new Registro { Descricao = "Valor Total", Data = "", Valor=ValorTotal, Tipo = -1 });

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
