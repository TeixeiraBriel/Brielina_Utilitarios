using Apresentacao.Controladores;
using Apresentacao.Views.Steam;

namespace Apresentacao
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            new ControladorInicializa().Inicializa();
        }

        private void NavigateSteam(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new HomeSteam());
        }
    }
}