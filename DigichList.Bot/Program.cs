
using DigichList.Bot.Configuration;
using DigichList.Core.Entities;
using DigichList.Core.Entities.Base;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Seeders;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Telegram.Bot;

namespace DigichList.Bot
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient(BotConfig.BotToken);
        static void Main(string[] args)
        {
            //Bot.OnMessage += Bot_OnMessage; 
            //Bot.StartReceiving();
            //Console.ReadLine();
            //Bot.StopReceiving();

        }
    }
}
