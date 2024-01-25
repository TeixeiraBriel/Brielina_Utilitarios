
using Dominio.Entidades;

namespace Dominio.Interfaces.Servicos
{
    public interface ISteamService
    {
        Task<Jogos> minerarDados(string idSteam, bool jogosGratis = true);
    }
}