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
    public class ControladorFinanceiro : Controlador
    {
        private string _UrlBrielinaApi = ConfigurationManager.AppSettings["UrlBrielinaApi"];

        public void cadastroRegistro(Registro entrada)
        {
            var client = new RestClient(_UrlBrielinaApi + "Financeiro/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            string param = JsonConvert.SerializeObject(entrada);
            request.AddParameter("application/json", param, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

        public void EditarRegistro(Registro entrada)
        {
            var client = new RestClient(_UrlBrielinaApi + "Financeiro/" + entrada.Id);
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            string param = JsonConvert.SerializeObject(entrada);
            request.AddParameter("application/json", param, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

        public List<Registro> buscarRegistros()
        {
            var client = new RestClient(_UrlBrielinaApi + "Financeiro/");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            List<Registro> registros = JsonConvert.DeserializeObject<List<Registro>>(response.Content);
            return registros;
        }

        public Registro buscarRegistro(int id)
        {
            var client = new RestClient(_UrlBrielinaApi + "Financeiro/" + id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            Registro registro = JsonConvert.DeserializeObject<Registro>(response.Content);

            return registro;
        }

        public void deleteRegistro(int id)
        {
            var client = new RestClient(_UrlBrielinaApi + "Financeiro/" + id);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
        }
    }
}
