using BrielinaUtilitarios.Controladores;
using Infraestrutura.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace BrielinaUtilitarios.Janelas.Contador
{
    /// <summary>
    /// Interação lógica para Inicio.xam
    /// </summary>
    public partial class Inicio : Page
    {
        public Inicio()
        {
            InitializeComponent();
            preencherTabela();
        }

        public void preencherTabela()
        {
            var file = @"Dados\Contador\contadorHistorico.json";
            List<ContadorHistorico> data = JsonConvert.DeserializeObject<List<ContadorHistorico>>(File.ReadAllText(file, Encoding.UTF8));
            TabelaContador.ItemsSource = agruparAtividades(data);
        }

        public List<ContadorHistorico> agruparAtividades(List<ContadorHistorico> historico)
        {
            List<ContadorHistorico> historicoAgrupado = new List<ContadorHistorico>();
            var excluir = historico.RemoveAll(i => i.tempo == "00:00:00");

            foreach (var atividade in historico)
            {
                var dado = historicoAgrupado.Find(i => i.janela == atividade.janela);
                if (dado == null)
                {
                    historicoAgrupado.Add(atividade);
                }
                else
                {
                    historicoAgrupado.Remove(historicoAgrupado.Find(i => i.janela == atividade.janela));

                    var dadotempo = DateTime.Parse(dado.tempo);
                    var atividadetempo = DateTime.Parse(atividade.tempo);

                    int newSec = 0;
                    int newMin = 0;
                    int newHour = 0;

                    if (dadotempo.Second + atividadetempo.Second >= 60)
                    {
                        newSec = (dadotempo.Second + atividadetempo.Second) - 60;
                        newMin += 1;
                    }
                    else
                    {
                        newSec += dadotempo.Second + atividadetempo.Second;

                    }

                    if (dadotempo.Minute + atividadetempo.Minute >= 60)
                    {
                        newMin += (dadotempo.Minute + atividadetempo.Minute) - 60;
                        newHour += 1;
                    }
                    else
                    {
                        newMin += dadotempo.Minute + atividadetempo.Minute;

                    }

                    dado.tempo = new DateTime(2000, 1, 1, newHour, newMin, newSec).ToString("HH:mm:ss");

                    historicoAgrupado.Add(dado);
                }
            }

            return historicoAgrupado;
        }

    }
}