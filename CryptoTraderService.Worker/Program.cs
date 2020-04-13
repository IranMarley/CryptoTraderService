using CryptoTraderService.Worker.Interfaces;
using CryptoTraderService.Worker.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CryptoTraderService.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddTransient<IRequest, Request>();
                    services.AddTransient<ITrade, Trade>();
                })
                .UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));
    }
}
