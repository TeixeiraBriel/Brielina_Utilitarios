using Apresentacao.Views.Steam;
using Dominio.Servicos;
using InjecaoDependecia;
using Microsoft.Extensions.Logging;

namespace Apresentacao
{
    #region new
    /*
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
            => MauiApp.CreateBuilder()
                .UseMauiApp<App>()
                // Omitted for brevity            
                .RegisterAppServices()
                .RegisterViewModels()
                .RegisterViews()
                .Build();
    }
    */
    #endregion

    #region default

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var services = builder.Services;
            var configuration = builder.Configuration;

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Dependency Injection
            _ = new Bootstrapper(services, configuration);
            
            //Views
            services.AddSingleton<HomeSteam>();

            #if DEBUG
            builder.Logging.AddDebug();
            #endif  

            return builder.Build();
        }
    }

    #endregion
}