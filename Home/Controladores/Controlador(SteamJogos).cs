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
    public partial class ControladorSteamJogos : Controlador
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
            var client = new RestClient($"https://api.steampowered.com/");
            var request = new RestRequest("ISteamUser/GetPlayerSummaries/v0002/?key=94FADF560260531F6AEDF05CD43EEC9B&steamids={idSteam}", Method.Get);
            var response = client.Execute(request);

            OwnedGames resposta = JsonConvert.DeserializeObject<OwnedGames>(response.Content);
            return resposta;
        }

        private OwnedGames req(string idSteam, bool? jogosGratis)
        {
            var client = new RestClient($"http://api.steampowered.com/");
            var request = new RestRequest("IPlayerService/GetOwnedGames/v0001/?key=94FADF560260531F6AEDF05CD43EEC9B&steamid={idSteam}&include_appinfo=true&include_played_free_games={jogosGratis}&format=json" ,
                Method.Get);
            var response = client.Execute(request);

            OwnedGames resposta = JsonConvert.DeserializeObject<OwnedGames>(response.Content);
            return resposta;
        }
    }
}
