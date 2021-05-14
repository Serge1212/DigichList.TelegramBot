﻿using static DigichList.Application.Helpers.TelegramBotTextMessages;
using static DigichList.Application.Helpers.TelegramBotMessageSender;
using DigichList.Application.Configuration;
using DigichList.Application.Interfaces;
using DigichList.Core.Entities;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace DigichList.Application.Helpers
{
    public class DefectStatusHandler : IDefectStatusHandler
    {
        private readonly ITelegramBotCommands _telegramBotCommands;
        public DefectStatusHandler(ITelegramBotCommands telegramBotCommands)
        {
            _telegramBotCommands = telegramBotCommands;
        }
        public async Task SendKeyboardWithStatuses(int telegramId, int assignedDefectId)
        {
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                    new KeyboardButton[][]
                    {
                        new KeyboardButton[] { "Виправляється" },
                        new KeyboardButton[] { "Завершено" },
                    },
                    resizeKeyboard: true,
                    oneTimeKeyboard: true
                );

            await TelegramBotEntity.Bot.SendTextMessageAsync(
                    chatId: telegramId,
                    text: "Виберіть статус дефекта",
                    replyMarkup: replyKeyboardMarkup
                );

            TelegramBotEntity.Bot.OnMessage += (sender, e) => OnStatusSelect(sender, e, telegramId, assignedDefectId);
        }

        private async void OnStatusSelect(object sender, Telegram.Bot.Args.MessageEventArgs e, int telegramId, int assignedDefectId)
        {
            if (telegramId != e.Message.From.Id) return;
            if (telegramId != e.Message.Chat.Id) return;

            var status = GetStatus(e.Message.Text);
            if(status != Status.Opened)
            {
                await _telegramBotCommands.SetDefectStatusAsync(telegramId, assignedDefectId, status);
                TelegramBotEntity.Bot.OnMessage -= (sender, e) => OnStatusSelect(sender, e, telegramId, assignedDefectId);
            }
            else
            {
                await SendMessageAsync(telegramId, InvalidStatus);
            }
        }

        private Status GetStatus(string statusText)
        {
            if (statusText == "Виправляється")
            {
                return Status.Fixing;
            }
            else if (statusText == "Завершено")
            {
                return Status.Done;
            }
            return Status.Opened;
        }
    }
}
