using Infraestrutura.Entidades;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes
{
    public class Controlador
    {
        public void cadastroAnimes(Anime entrada)
        {
            var client = new RestClient("http://localhost:54830/api/animes/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            string param = JsonConvert.SerializeObject(entrada);
            request.AddParameter("application/json", param, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

        public void EditarAnime(Anime entrada)
        {
            var client = new RestClient("http://localhost:54830/api/animes/" + entrada.Id);
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            string param = JsonConvert.SerializeObject(entrada);
            request.AddParameter("application/json", param, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

        public List<Anime> buscarAnimes()
        {
            var client = new RestClient("http://localhost:54830/api/animes/");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            List<Anime> Animes = JsonConvert.DeserializeObject<List<Anime>>(response.Content);
            return Animes;
        }

        public Anime buscarAnime(int id)
        {
            var client = new RestClient("http://localhost:54830/api/animes/" + id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            Anime Anime = JsonConvert.DeserializeObject<Anime>(response.Content);

            return Anime;
        }

        public void deleteAnime(int id)
        {
            var client = new RestClient("http://localhost:54830/api/animes/" + id);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
        }
    }
}