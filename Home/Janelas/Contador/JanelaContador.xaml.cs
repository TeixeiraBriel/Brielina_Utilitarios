﻿using BrielinaUtilitarios.Controladores;
using BrielinaUtilitarios.Janelas.Controles;
using BrielinaUtilitarios.Util;
using Home;
using Infraestrutura.Entidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace BrielinaUtilitarios.Janelas.Contador
{
    /// <summary>
    /// Lógica interna para JanelaContador.xaml
    /// </summary>
    public partial class JanelaContador : Window
    {
        internal static void InicializaMaximizar() => Instancia.Maximilizar.ChangeState += Instancia.ChangeWindowState;
        ControladorContador _controlador;

        MainWindow janelaPrincipal;

        public static JanelaContador Instancia;
        public static void Fechar() => Instancia?.Close();
        public static void Mostrar() => Instancia?.Show();
        public static void Focar() => Instancia?.Activate();
        public static void Inicializa(MainWindow JanelaPrincipal)
        {
            Instancia = new JanelaContador(JanelaPrincipal)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowState = WindowState.Normal,
            };
            InicializaMaximizar();
        }

        public JanelaContador(MainWindow JanelaPrincipal)
        {
            InitializeComponent();
            inicializaTimer();
            janelaPrincipal = JanelaPrincipal;
            _controlador = new ControladorContador();
        }

        private void ChangeState(object sender, RoutedEventArgs e) => ChangeWindowState(false);

        public void ChangeWindowState(bool maximizar)
        {
            var branco = (SolidColorBrush)new BrushConverter().ConvertFromString("White");
            var roxo = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF673AB7");
            var fillSeparator = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE0E0E0");
            DispatcherUtil.Dispatcher(() =>
            {
                if (maximizar)
                {
                    ChangeSizeWindow(500, 400);
                    AtualizaEstado();
                    Maximilizar.FechaModal();
                    BordaPrincipal.Background = branco;
                    BordaPrincipal.BorderBrush = fillSeparator;
                    Timer.Inicia();
                }
                else
                {
                    Timer.Para();
                    ChangeSizeWindow(30, 100);
                    AtualizaEstado();
                    Maximilizar.AbreModal();
                    BordaPrincipal.Background = roxo;
                    BordaPrincipal.BorderBrush = roxo;
                }
            });
        }
        public void ChangeSizeWindow(double width, double height)
        {
            Height = height;
            Width = width;
        }

        private void AtualizaEstado()
        {
            var top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2;
            var left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Top = top;
            this.Left = left;
        }

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public int contadorSegundos = 0;
        public int contadorMinutos = 0;
        public int contadorHoras = 0;
        private string ultimaJanelaAtiva = "";
        public List<ContadorHistorico> historicos = new List<ContadorHistorico>();

        public void inicializaTimer()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var janelaAtiva = _controlador.capturaJanelaAtiva();
            if (ultimaJanelaAtiva == "")
            {
                ultimaJanelaAtiva = janelaAtiva;
            }
            if (janelaAtiva != ultimaJanelaAtiva)
            {
                DateTime tempo = new DateTime(2000, 1, 1, contadorMinutos, contadorMinutos, contadorSegundos);
                GravarLinhaJson(new ContadorHistorico { janela = ultimaJanelaAtiva, tempo = tempo.ToString("HH:mm:ss") });
                ZerarContadorFunc();
                ultimaJanelaAtiva = janelaAtiva;
            }

            JanelaAtiva.Text = janelaAtiva;
            contadorSegundos++;

            if (contadorSegundos >= 60)
            {
                contadorSegundos = 0;
                contadorMinutos++;
            }
            if (contadorMinutos >= 60)
            {
                contadorMinutos = 0;
                contadorHoras++;
            }
            TesteCampo.Text = $"{contadorHoras  } Horas - {contadorMinutos} Minutos - {contadorSegundos} Segundos";
        }

        public void ZerarContadorFunc()
        {
            contadorSegundos = 0;
            contadorMinutos = 0;
            contadorHoras = 0;
            TesteCampo.Text = $"{contadorMinutos} Horas - {contadorMinutos} Minutos - {contadorSegundos} Segundos";
        }

        public void ZerarDadosFunc()
        {
            List<ContadorHistorico> historicoContador = new List<ContadorHistorico>();
            string output = JsonConvert.SerializeObject(historicoContador);

            File.WriteAllText(@"Dados\Contador\contadorHistorico.json", output.ToString());
            preencherTabela(historicoContador);
        }

        private void GravarLinhaJson(ContadorHistorico novaLinha)
        {
            List<ContadorHistorico> historicoContador = new List<ContadorHistorico>();

            var file = @"Dados\Contador\contadorHistorico.json";
            List<ContadorHistorico> data = JsonConvert.DeserializeObject<List<ContadorHistorico>>(File.ReadAllText(file, Encoding.UTF8));

            try
            {
                foreach (var dado in data)
                {
                    historicoContador.Add(dado);
                }
            }
            catch
            {
                historicoContador.Add(novaLinha);
            }

            historicoContador.Add(novaLinha);

            string output = JsonConvert.SerializeObject(historicoContador);

            File.WriteAllText(@"Dados\Contador\contadorHistorico.json", output.ToString());
            preencherTabela(historicoContador);
        }

        public void preencherTabela(List<ContadorHistorico> historico)
        {
            TabelaContador.ItemsSource = agruparAtividades(historico);
        }

        public List<ContadorHistorico> agruparAtividades(List<ContadorHistorico> historico)
        {
            List<ContadorHistorico> historicoAgrupado = new List<ContadorHistorico>();

            foreach (var atividade in historico)
            {
                var dado = historicoAgrupado.Find(i => i.janela == atividade.janela);
                if ( dado == null)
                {
                    historicoAgrupado.Add(atividade);
                }
                else
                {
                    historicoAgrupado.Remove(historicoAgrupado.Find(i => i.janela == atividade.janela));
                   
                    var dadotempo = DateTime.Parse(dado.tempo);
                    var atividadetempo = DateTime.Parse(atividade.tempo);

                    dado.tempo = new DateTime(2000, 1, 1, dadotempo.Hour + atividadetempo.Hour, dadotempo.Minute + atividadetempo.Minute, dadotempo.Second + atividadetempo.Second).ToString("HH:mm:ss");

                    historicoAgrupado.Add(dado);
                }
            }

            return historicoAgrupado;
        }

        private void PararContador(object sender, RoutedEventArgs e)
        {
            if (dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Stop();
                PararContadorBtn.Content = "Iniciar Contador";
            }
            else
            {
                dispatcherTimer.Start();
                PararContadorBtn.Content = "Pausar Contador";
            }
        }
        private void ZerarContador(object sender, RoutedEventArgs e)
        {
            ZerarDadosFunc();
        }
        private void FecharContador(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            this.Close();
            janelaPrincipal.WindowState = WindowState.Normal;
        }
    }
}