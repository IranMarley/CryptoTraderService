﻿using RestSharp;
using System.Threading.Tasks;

namespace CryptoTraderService.Worker.Interfaces
{
    public interface ITrade
    {
        T CallEndpoint<T>(string endpoint, string pair);
        T CallEndpoint<T>(string endpoint, string pair, int amount);
        T CallEndpoint<T>(string endpoint, Method method, string json);
        Task Execute();
    }
}