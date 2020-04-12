using System.Threading.Tasks;

namespace CryptoTraderService.Worker.Interfaces
{
    public interface ITrade
    {
        Task Operation();
    }
}