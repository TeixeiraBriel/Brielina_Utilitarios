/*
using Dominio.Entidades;
using Dominio.Servicos;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Infraestrutura.Servicos
{
    public class SteamService : ISteamService
    {
        public async Task<OwnedGames> minerarDados(string idSteam, bool jogosGratis = true)
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

        public async Task<OwnedGames> minerarPerfil(string idSteam)
        {
            var client = new RestClient($"https://api.steampowered.com/");
            var request = new RestRequest($"ISteamUser/GetPlayerSummaries/v0002/?key=94FADF560260531F6AEDF05CD43EEC9B&steamids={idSteam}", Method.Get);
            var response = await client.ExecuteAsync(request);

            OwnedGames resposta = JsonConvert.DeserializeObject<OwnedGames>(response.Content);
            return resposta;
        }

        private async Task<OwnedGames> req(string idSteam, bool? jogosGratis)
        {

            var client = new RestClient($"http://api.steampowered.com/");
            var request = new RestRequest($"IPlayerService/GetOwnedGames/v0001/?key=94FADF560260531F6AEDF05CD43EEC9B&steamid={idSteam}&include_appinfo=true&include_played_free_games={jogosGratis}&format=json",
                Method.Get);
            var response = await client.ExecuteAsync(request);

            OwnedGames resposta = JsonConvert.DeserializeObject<OwnedGames>(response.Content);
            return resposta;
        }
    }
}
*/