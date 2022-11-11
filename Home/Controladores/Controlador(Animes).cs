using BrielinaUtilitarios.Controladores;
using Infraestrutura.Entidades;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Configuration;

namespace Home.Controladores
{
    public partial class ControladorAnimes : Controlador
    {
        private string _UrlBrielinaApi = ConfigurationManager.AppSettings["UrlBrielinaApi"];

        public void cadastroAnimes(Anime entrada)
        {
            var client = new RestClient(_UrlBrielinaApi);
            var request = new RestRequest("animes/", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            string param = JsonConvert.SerializeObject(entrada);
            request.AddParameter("application/json", param, ParameterType.RequestBody);
            var response = client.Execute(request);
        }

        public void EditarAnime(Anime entrada)
        {
            var client = new RestClient(_UrlBrielinaApi);
            var request = new RestRequest("animes/" + entrada.Id, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            string param = JsonConvert.SerializeObject(entrada);
            request.AddParameter("application/json", param, ParameterType.RequestBody);
            var response = client.Execute(request);
        }

        public List<Anime> buscarAnimes()
        {
            var client = new RestClient(_UrlBrielinaApi);
            var request = new RestRequest("animes/", Method.Get);
            var response = client.Execute(request);

            List<Anime> Animes = JsonConvert.DeserializeObject<List<Anime>>(response.Content);
            return Animes;
        }

        public Anime buscarAnime(int id)
        {
            var client = new RestClient(_UrlBrielinaApi);
            var request = new RestRequest("animes/" + id, Method.Get);
            var response = client.Execute(request);

            Anime Anime = JsonConvert.DeserializeObject<Anime>(response.Content);

            return Anime;
        }

        public void deleteAnime(int id)
        {
            var client = new RestClient(_UrlBrielinaApi);
            var request = new RestRequest("animes/" + id, Method.Delete);
            var response = client.Execute(request);
        }
    }
}