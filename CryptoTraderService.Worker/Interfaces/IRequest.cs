using RestSharp;

namespace CryptoTraderService.Worker.Interfaces
{
    public interface IRequest
    {
        string SendRequest(string url, string token, string json, Method method, bool tokenTypeAuthorization = false);
    }
}