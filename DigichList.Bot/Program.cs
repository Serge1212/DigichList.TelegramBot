using DigichList.Application.Configuration;
using DigichList.Application.Interfaces;
using DigichList.Application.Services;
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
            Bot.OnMessage += Bot_OnMessage;
            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            await new TelegramBotCommandsHandler(new TelegramBotCommandsService(new UserRepository(new DigichListContext())))
                .HandleCommands(e.Message);
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
                  .AddScoped<ITelegramBotCommands, TelegramBotCommandsService>()
                  .AddDbContext<DigichListContext>();
              }).UseConsoleLifetime();

            var host = builder.Build();
        }
    }
}
