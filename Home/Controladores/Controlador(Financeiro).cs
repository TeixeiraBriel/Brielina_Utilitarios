using BrielinaFinanceiro.Entidades;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrielinaUtilitarios.Controladores
{
    public partial class ControladorFinanceiro : Controlador
    {
        private string _UrlBrielinaApi = ConfigurationManager.AppSettings["UrlBrielinaApi"];

        public void cadastroRegistro(Registro entrada)
        {
            var client = new RestClient(_UrlBrielinaApi);
            var request = new RestRequest("Financeiro/", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            string param = JsonConvert.SerializeObject(entrada);
            request.AddParameter("application/json", param, ParameterType.RequestBody);
            var response = client.Execute(request);
        }

        public void EditarRegistro(Registro entrada)
        {
            var client = new RestClient(_UrlBrielinaApi);
            var request = new RestRequest("Financeiro/" + entrada.Id, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            string param = JsonConvert.SerializeObject(entrada);
            request.AddParameter("application/json", param, ParameterType.RequestBody);
            var response = client.Execute(request);
        }

        public List<Registro> buscarRegistros()
        {
            var client = new RestClient(_UrlBrielinaApi);
            var request = new RestRequest("Financeiro/", Method.Get);
            var response = client.Execute(request);

            List<Registro> registros = JsonConvert.DeserializeObject<List<Registro>>(response.Content);
            return registros;
        }

        public Registro buscarRegistro(int id)
        {
            var client = new RestClient(_UrlBrielinaApi);
            var request = new RestRequest("Financeiro/" + id, Method.Get);
            var response = client.Execute(request);

            Registro registro = JsonConvert.DeserializeObject<Registro>(response.Content);

            return registro;
        }

        public void deleteRegistro(int id)
        {
            var client = new RestClient(_UrlBrielinaApi);
            var request = new RestRequest("Financeiro/" + id, Method.Delete);
            var response = client.Execute(request);
        }
    }
}
