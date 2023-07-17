using Dominio.Entidades;

namespace Dominio.Servicos
{
    public interface ISteamService
    {
        Task<OwnedGames> minerarDados(string idSteam, bool jogosGratis = true);
    }
}
