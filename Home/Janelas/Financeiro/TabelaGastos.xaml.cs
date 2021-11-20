using System.Collections.Generic;
using System.Windows.Controls;
using BrielinaFinanceiro.Entidades;
using BrielinaUtilitarios.Controladores;

namespace BrielinaUtilitarios.Janelas.Financeiro
{
    /// <summary>
    /// Interação lógica para TabelaGastos.xam
    /// </summary>
    public partial class TabelaGastos : Page
    {
        ControladorFinanceiro _controlador;

        public TabelaGastos()
        {
            InitializeComponent();
            _controlador = new ControladorFinanceiro();

            List<Registro> registros = _controlador.buscarRegistros();

            foreach (var registro in registros)
            {
                if (registro.Tipo == 1)
                {
                    DataGridGastos.Items.Add(registro);
                }
            }
        }
    }
}
