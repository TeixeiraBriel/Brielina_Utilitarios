using Dominio.Entidades;

namespace Dominio.Interfaces.Servicos
{
    public interface IMetasService
    {
        Task<Metas> BuscarMetas();
        Task CriarMeta(Meta meta);
    }
}