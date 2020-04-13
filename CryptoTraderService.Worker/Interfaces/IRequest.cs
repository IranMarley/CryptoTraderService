using RestSharp;
using System.Threading.Tasks;

namespace CryptoTraderService.Worker.Interfaces
{
    public interface IRequest
    {
        T SendRequest<T>(string url, string token, string json, Method method, bool tokenTypeAuthorization = false);
        Task<T> SendRequestAsync<T>(string url, string token, string json, Method method, bool tokenTypeAuthorization = false);
    }
}