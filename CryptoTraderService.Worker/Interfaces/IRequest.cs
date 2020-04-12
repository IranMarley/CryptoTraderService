using RestSharp;

namespace CryptoTraderService.Worker.Services
{
    public interface IRequest
    {
        string SendRequest(string url, string token, string json, Method method, bool tokenTypeAuthorization = false);
    }
}