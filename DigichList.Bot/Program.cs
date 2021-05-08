using DigichList.Application.Configuration;
using DigichList.Application.Interfaces;
using DigichList.Application.Services;
using DigichList.Bot.Handlers;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Telegram.Bot;

namespace DigichList.Bot
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient(BotConfig.BotToken);
        static void Main(string[] args)
        {
            ConfigureServices();
            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static void ConfigureServices()
        {
            Console.WriteLine("I was started");
            var builder = new HostBuilder()
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddLogging(configure => configure.AddConsole())
                  .AddScoped<IDefectImageRepository, DefectImageRepository>()
                  .AddScoped<IUserRepository, UserRepository>()
                  .AddScoped<IDefectRepository, DefectRepository>()
                  .AddScoped<IRoleRepository, RoleRepository>()
                  .AddScoped<ITelegramBotCommands, TelegramBotCommandsService>()
                  .AddScoped<TelegramBotCommandsHandler>()
                  .AddDbContext<DigichListContext>();
              }).UseConsoleLifetime();

            var host = builder.Build();

            var serviceScope = host.Services.CreateScope();
            
            var services = serviceScope.ServiceProvider;

            try
            {
                var myService = services.GetRequiredService<TelegramBotCommandsHandler>();
                Bot.OnMessage += myService.Bot_OnMessage;

                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured: {ex}");
            }
            
        }
    }
}
