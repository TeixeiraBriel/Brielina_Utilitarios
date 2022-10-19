using BrielinaUtilitarios.Controladores;
using BrielinaUtilitarios.Janelas.Controles;
using BrielinaUtilitarios.Util;
using Home;
using Infraestrutura.Entidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                }
                else
                {
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
        public TimeSpan contadorData = new TimeSpan(0, 0, 0);
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
            try
            {
                var janelaAtiva = _controlador.capturaJanelaAtiva();
                if (ultimaJanelaAtiva == "")
                {
                    ultimaJanelaAtiva = janelaAtiva;
                }
                if (janelaAtiva != ultimaJanelaAtiva)
                {
                    if (ultimaJanelaAtiva != "Contador")
                    {
                        if (ultimaJanelaAtiva.Contains("- Google Chrome"))
                            ultimaJanelaAtiva = "Google Chrome";

                        GravarLinhaJson(new ContadorHistorico { janela = ultimaJanelaAtiva, tempo = contadorData.ToString() });
                    }
                    ZerarContadorFunc();
                    ultimaJanelaAtiva = janelaAtiva;
                }

                JanelaAtiva.Text = janelaAtiva;
                contadorData += TimeSpan.FromSeconds(1);

                TesteCampo.Text = $"{contadorData.Hours} Horas - {contadorData.Minutes} Minutos - {contadorData.Seconds} Segundos";
            }
            catch (Exception ex)
            {
                string messageBoxText = ex.Message;
                string caption = "Erro ao executar";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon);

                dispatcherTimer.Stop();
                this.Close();
                janelaPrincipal.WindowState = WindowState.Normal;
            }
        }

        public void ZerarContadorFunc()
        {
            contadorData = new TimeSpan(0, 0, 0);
            TesteCampo.Text = $"{contadorData.Hours} Horas - {contadorData.Minutes} Minutos - {contadorData.Seconds} Segundos";
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
            var file = @"Dados\Contador\contadorHistorico.json";
            List<ContadorHistorico> data = JsonConvert.DeserializeObject<List<ContadorHistorico>>(File.ReadAllText(file, Encoding.UTF8));

            var linha = data.FirstOrDefault(x => x.janela == novaLinha.janela);
            if (linha == null)
            {
                data.Add(novaLinha);
            }
            else
            {
                var numerosAntigos = linha.tempo.Split(':');
                var numerosNovos = novaLinha.tempo.Split(':');

                TimeSpan novoTempo = new TimeSpan();
                novoTempo += TimeSpan.FromSeconds(double.Parse(numerosAntigos[2]));   
                novoTempo += TimeSpan.FromSeconds(double.Parse(numerosNovos[2]));
                novoTempo += TimeSpan.FromMinutes(double.Parse(numerosAntigos[1]));
                novoTempo += TimeSpan.FromMinutes(double.Parse(numerosNovos[1]));
                novoTempo += TimeSpan.FromHours(double.Parse(numerosAntigos[0]));
                novoTempo += TimeSpan.FromHours(double.Parse(numerosNovos[0]));

                linha.tempo = novoTempo.ToString();
            }
            

            string output = JsonConvert.SerializeObject(data);

            File.WriteAllText(@"Dados\Contador\contadorHistorico.json", output.ToString());
            preencherTabela(data);
        }

        public void preencherTabela(List<ContadorHistorico> historico)
        {
            TabelaContador.ItemsSource = historico;
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
            GravarLinhaJson(new ContadorHistorico { janela = ultimaJanelaAtiva, tempo = contadorData.ToString() });
            this.Close();

            this.janelaPrincipal.janelaPrincipal.NavigationService.Navigate(new Contador.Inicio());
            janelaPrincipal.WindowState = WindowState.Normal;
        }

        private void MudarVisibilidade(object sender, RoutedEventArgs e)
        {
            bool resposta = Sobrepor.IsChecked == true ? true : false;
            if (resposta)
                Sobrepor.Content = "Sobrepondo";
            else
                Sobrepor.Content = "Escondendo";

            this.Topmost = resposta;
        }
    }
}