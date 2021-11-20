using Infraestrutura.Entidades;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrielinaUtilitarios.Controladores
{
    public class ControladorSteamJogos
    {
        public OwnedGames minerarDados(string idSteam, bool? jogosGratis)
        {
            try
            {
                return req(idSteam, jogosGratis);
            }
            catch
            {
                return null;
            }
        }

        public OwnedGames minerarPerfil(string idSteam)
        {
            var client = new RestClient($"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=94FADF560260531F6AEDF05CD43EEC9B&steamids={idSteam}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            OwnedGames resposta = JsonConvert.DeserializeObject<OwnedGames>(response.Content);
            return resposta;
        }

        private OwnedGames req(string idSteam, bool? jogosGratis)
        {
            var client = new RestClient($"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=94FADF560260531F6AEDF05CD43EEC9B&steamid={idSteam}&include_appinfo=true&include_played_free_games={jogosGratis}&format=json");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            OwnedGames resposta = JsonConvert.DeserializeObject<OwnedGames>(response.Content);
            return resposta;
        }
    }
}
