using Dominio.Configuration;
using Dominio.Servicos;
using Infraestrutura.Servicos;
using InjecaoDependencia.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InjecaoDependecia
{
    public class Bootstrapper
    {
        public Bootstrapper(IServiceCollection services, IConfiguration configuration)
        {
            //Entity
            //services.AddConfiguration<AppSettings>(configuration);

            //Services
            services.AddSingleton<ISteamService, SteamService>();
        }
    }
}
