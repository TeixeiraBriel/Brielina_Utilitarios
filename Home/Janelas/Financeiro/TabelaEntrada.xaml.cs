using System.Collections.Generic;
using System.Windows.Controls;
using BrielinaFinanceiro.Entidades;
using BrielinaUtilitarios.Controladores;

namespace BrielinaUtilitarios.Janelas.Financeiro
{
    /// <summary>
    /// Interação lógica para TabelaEntrada.xam
    /// </summary>
    public partial class TabelaEntrada : Page
    {
        ControladorFinanceiro _controlador;

        public TabelaEntrada()
        {
            InitializeComponent();
            _controlador = new ControladorFinanceiro();

            List<Registro> registros = _controlador.buscarRegistros();

            foreach (var registro in registros)
            {
                if (registro.Tipo == 0)
                {
                    DataGridEntrada.Items.Add(registro);
                }
            }
        }
    }
}
