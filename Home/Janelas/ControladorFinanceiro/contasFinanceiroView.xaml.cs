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

namespace BrielinaUtilitarios.Janelas.ControladorFinanceiro
{
    /// <summary>
    /// Interação lógica para contasFinanceiroView.xam
    /// </summary>
    public partial class contasFinanceiroView : Page
    {
        public contasFinanceiroView()
        {
            InitializeComponent();
            preencherTabela();
        }

        public void preencherTabela()
        {
            List<ContaFinanceiro> dadosVenda = buscaContas();
            double valorTotalContas = 0;

            foreach (var item in dadosVenda)
            {
                TextBox textoNome = new TextBox { Text = item.nome, IsReadOnly = true };
                nome.Children.Add(textoNome);

                TextBox textoContato = new TextBox { Text = item.valorContido.ToString(), IsReadOnly = true };
                valorConta.Children.Add(textoContato);
                valorTotalContas = valorTotalContas + item.valorContido;
            }

            TextBox txtNome = new TextBox { Text = "Total", IsReadOnly = true };
            nome.Children.Add(txtNome);

            TextBox txtValor = new TextBox { Text = valorTotalContas.ToString(), IsReadOnly = true };
            valorConta.Children.Add(txtValor);
        }

        public List<ContaFinanceiro> buscaContas()
        {
            List<ContaFinanceiro> contas = new List<ContaFinanceiro>();

            contas.Add(new ContaFinanceiro { nome = "Bradesco", valorContido = 150.99 });
            contas.Add(new ContaFinanceiro { nome = "Super Digital", valorContido = 50.99 });
            return contas;
        }
    }
}
