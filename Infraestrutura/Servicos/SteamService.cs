using Dominio.Entidades;
using Dominio.Interfaces.Servicos;
using Newtonsoft.Json;
using RestSharp;

namespace Infraestrutura.Servicos
{
    public class SteamService : ISteamService
    {
        public async Task<Jogos> minerarDados(string idSteam, bool jogosGratis = true)
        {
            try
            {
                return await req(idSteam, jogosGratis);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<Jogos> minerarPerfil(string idSteam)
        {
            var client = new RestClient($"https://api.steampowered.com/");
            var request = new RestRequest($"ISteamUser/GetPlayerSummaries/v0002/?key=94FADF560260531F6AEDF05CD43EEC9B&steamids={idSteam}", Method.Get);
            var response = await client.ExecuteAsync(request);

            OwnedGames resposta = JsonConvert.DeserializeObject<OwnedGames>(response.Content);
            return resposta.response;
        }

        private async Task<Jogos> req(string idSteam, bool? jogosGratis)
        {
            var client = new RestClient($"http://api.steampowered.com/");
            var request = new RestRequest($"IPlayerService/GetOwnedGames/v0001/?key=94FADF560260531F6AEDF05CD43EEC9B&steamid={idSteam}&include_appinfo=true&include_played_free_games={jogosGratis}&format=json",
                Method.Get);
            var response = await client.ExecuteAsync(request);

            OwnedGames resposta = JsonConvert.DeserializeObject<OwnedGames>(response.Content);
            return resposta.response;
        }
    }
}