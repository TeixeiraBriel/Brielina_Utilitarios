namespace BrielinaUtilitarios.Janelas.Controles
{
    using System.Windows;
    using System.Windows.Controls;
    using System;
    using WpfAppBar;

    /// <summary>
    /// Interaction logic for Maximizar.xaml
    /// </summary>
    public partial class Maximizar : UserControl
    {
        public event Action<bool> ChangeState;
        public Maximizar()
        {
            InitializeComponent();
        }

        public void AbreModal() => Visibility = Visibility.Visible;

        public void FechaModal() => Visibility = Visibility.Collapsed;

        private void Change(object sender, RoutedEventArgs e)
        {
            AbreModal();
            ChangeState(true);
        }
    }
}
