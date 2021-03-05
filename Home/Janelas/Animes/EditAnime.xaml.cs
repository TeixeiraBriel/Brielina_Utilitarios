using Animes;
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

namespace Home.Janelas.Animes
{
    /// <summary>
    /// Interação lógica para EditAnime.xam
    /// </summary>
    public partial class EditAnime : Page
    {
        private Page JanelaAnterior;
        private Controlador _controlador;
        private int Id;

        public EditAnime(Page janelaAnterior, int id)
        {
            InitializeComponent();
            _controlador = new Controlador();
            JanelaAnterior = janelaAnterior;
            Id = id;
        }

        private void Voltar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(JanelaAnterior);
        }
    }
}
