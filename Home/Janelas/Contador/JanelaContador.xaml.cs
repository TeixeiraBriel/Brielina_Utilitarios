using BrielinaUtilitarios.Janelas.Controles;
using BrielinaUtilitarios.Util;
using Home;
using System;
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

        public void inicializaTimer()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            contadorSegundos++;

            if (contadorSegundos == 60)
            {
                contadorSegundos = 0;
                contadorMinutos++;
                dispatcherTimer.Stop();
            }
            if (contadorMinutos == 60)
            {
                contadorMinutos= 0;
                contadorHoras++;
            }
            TesteCampo.Text = $"{contadorMinutos} Horas - {contadorMinutos} Minutos - {contadorSegundos} Segundos";
        }

        private void PararContador(object sender, RoutedEventArgs e)
        {
            if (dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Stop();
                TesteButton.Content = "Iniciar Contador";
            }
            else
            {
                dispatcherTimer.Start();
                TesteButton.Content = "Pausar Contador";
            }
        }

        private void FecharContador(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            this.Close();
            janelaPrincipal.WindowState = WindowState.Normal;
        }
    }
}