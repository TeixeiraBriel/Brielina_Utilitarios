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
    }
}