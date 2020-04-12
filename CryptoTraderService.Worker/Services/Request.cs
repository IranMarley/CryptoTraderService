using CryptoTraderService.Worker.Interfaces;
using RestSharp;

namespace CryptoTraderService.Worker.Services
{
    public class Request : IRequest
    {
        public string SendRequest(string url, string token, string json, Method method, bool tokenTypeAuthorization = false)
        {
            var client = new RestClient(url);
            var request = new RestRequest(method);

            if (tokenTypeAuthorization)
                request.AddHeader("Authorization", $"ApiToken {token}");
            else
                request.AddHeader("x-api-key", token);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}