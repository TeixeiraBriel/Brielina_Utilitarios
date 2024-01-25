using Dominio.Interfaces.Servicos;
using Infraestrutura.Servicos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InjecaoDependencias
{
    public class Bootstrapper
    {
        public Bootstrapper(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ISteamService, SteamService>();
            services.AddSingleton<IMetasService, MetasService>();
        }
    }
}
