using Dominio.InjecaoDependencias;
using Dominio.Servicos;
using Infraestrutura.Servicos;
using System.Diagnostics.CodeAnalysis;
using Unity;
using Unity.Lifetime;

namespace InjecaoDependencias
{
    public sealed class Resolvedor : IResolvedor
    {
        //corrigir
        private readonly IUnityContainer _container;

        [SuppressMessage("Microsoft.Maintainability","CA1506:AvoidExcessiveClassCoupling", Justification = "O resolvedor é a classe aonde fica o 'acomplamento' da aplicação")]
        public Resolvedor()
        {
            _container = new UnityContainer();

            Singleton<ISteamService, SteamService>();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void Singleton<T>(T obj)
        {
            _container.RegisterInstance<T>(obj);
        }

        private void Singleton<T1, T2>() where T2 : T1
        {
            _container.RegisterType<T1, T2>(new ContainerControlledLifetimeManager());
        }
    }
}
