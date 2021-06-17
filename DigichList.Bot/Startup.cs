using DigichList.Application.Configuration;
using DigichList.Application.Helpers;
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

namespace DigichList.Bot
{
    class Startup
    { 
        static void Main(string[] args)
        {
            ConfigureServices();
            TelegramBotEntity.Bot.StartReceiving();
            Console.ReadLine();
            TelegramBotEntity.Bot.StopReceiving();
        }

        private static void ConfigureServices()
        {
            var builder = new HostBuilder()
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddLogging(configure => configure.AddConsole())
                  .AddScoped<IDefectImageRepository, DefectImageRepository>()
                  .AddScoped<IUserRepository, UserRepository>()
                  .AddScoped<IDefectRepository, DefectRepository>()
                  .AddScoped<IRoleRepository, RoleRepository>()
                  .AddScoped<IAssignedDefectRepository, AssignedDefectRepository>()
                  .AddScoped<ITelegramBotCommands, TelegramBotCommandsService>()
                  .AddScoped<INewDefectCommand, NewDefectCommand>()
                  .AddScoped<IManageDefectStatusCommand, ManageDefectStatusCommand>()
                  .AddScoped<IDefectStatusHandler, DefectStatusHandler>()
                  .AddScoped<TelegramBotCommandsHandler>()
                  .AddDbContext<DigichListContext>();
              });

            var host = builder.Build();

            var serviceScope = host.Services.CreateScope();
            
            var services = serviceScope.ServiceProvider;

            try
            {
                var telegramBotCommandsHandlerService = services.GetRequiredService<TelegramBotCommandsHandler>();
                TelegramBotEntity.Bot.OnMessage += telegramBotCommandsHandlerService.Bot_OnMessage;

                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured: {ex}");
            }
            
        }
    }
}
