using Apresentacao.Views.Steam;
using Dominio.Servicos;
using Infraestrutura.Servicos;

namespace Apresentacao
{
    public partial class App : Application
    {
        public App(HomeSteam mainpage)
        {
            InitializeComponent();

            MainPage = mainpage;
        }
    }
}