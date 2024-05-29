using System;
using System.Net.Http;
using System.Threading.Tasks;
using ConsumerAPI.Data;
using ConsumerAPI.Models;
using ConsumerAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TransferOrderClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Configuração do host
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var transferOrderService = services.GetRequiredService<TransferOrderService>();
                var databaseService = services.GetRequiredService<DatabaseService>();

                try
                {
                    var baseUrl = "https://localhost:7157";
                    var transferOrders = await transferOrderService.GetTransferOrdersAsync(baseUrl);

                    //Exibir os dados
                    foreach (var order in transferOrders)
                    {
                        Console.WriteLine($"Pedido: {order.Pedido}, OrderDate: {order.OrderDate}");
                    }

                    //Salvar os dados no DB
                    await databaseService.SaveTransferOrdersAsync(transferOrders);
                    Console.WriteLine("Dados salvos no SQL Server");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Erro: {e.Message}");
                }

            }


            static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    })
                    .ConfigureServices((context, services) =>
                    {
                        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                        services.AddDbContext<TransferOrderContext>(options =>
                            options.UseSqlServer(connectionString)
                                    .EnableSensitiveDataLogging()
                                    .LogTo(Console.WriteLine, LogLevel.Information));
                        services.AddSingleton<HttpClient>();
                        services.AddTransient<TransferOrderService>();
                        services.AddTransient<DatabaseService>();
                    })
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.AddDebug();
                    });

        }
    }
}
