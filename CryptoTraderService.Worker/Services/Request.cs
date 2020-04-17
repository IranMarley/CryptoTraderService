using CryptoTraderService.Worker.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace CryptoTraderService.Worker.Services
{
    public class Request : IRequest
    {
        public T SendRequest<T>(string url, string token, string json, Method method, bool tokenTypeAuthorization = false)
        {
            var client = new RestClient(url);
            var request = new RestRequest(method);

            AddHeaders(token, json, tokenTypeAuthorization, request);

            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public async Task<T> SendRequestAsync<T>(string url, string token, string json, Method method, bool tokenTypeAuthorization = false)
        {
            var client = new RestClient(url);
            var request = new RestRequest(method);

            AddHeaders(token, json, tokenTypeAuthorization, request);

            IRestResponse response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        private static void AddHeaders(string token, string json, bool tokenTypeAuthorization, RestRequest request)
        {
            if (tokenTypeAuthorization)
                request.AddHeader("Authorization", $"ApiToken {token}");
            else
                request.AddHeader("x-api-key", token);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
        }
    }
}