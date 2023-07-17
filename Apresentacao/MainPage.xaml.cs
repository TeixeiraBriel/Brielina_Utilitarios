using Apresentacao.Views.Steam;
using Dominio.Servicos;
using Infraestrutura.Servicos;

namespace Apresentacao
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        ISteamService _steamService;

        public MainPage(ISteamService steamService)
        {
            InitializeComponent();
            _steamService = steamService;
        }

        private void NavigateSteam(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new HomeSteam(_steamService));
            //Navigation.PushAsync();
        }
    }
}