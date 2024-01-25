using Dominio.Entidades;
using Dominio.Interfaces.Servicos;

namespace Infraestrutura.Servicos
{
    public class MetasService : IMetasService
    {
        private Metas metas;

        public MetasService()
        {
            metas = new Metas();
            metas.Objetivos.Add(new Meta() { Nome = "Teste", DataInicio = DateTime.Now, DataFim = DateTime.Now, DiasExecutados = new List<DateTime>() });
            metas.Objetivos.Add(new Meta() { Nome = "Teste2", DataInicio = DateTime.Now, DataFim = DateTime.Now, DiasExecutados = new List<DateTime>() });
        }

        public Task<Metas> BuscarMetas()
        {
            return Task.FromResult(metas);
        }

        public Task CriarMeta(Meta meta)
        {
            metas.Objetivos.Add(meta);
            return Task.CompletedTask;
        }
    }
}
