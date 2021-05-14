using DigichList.Application.Configuration;
using DigichList.Application.Interfaces;
using DigichList.Core.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace DigichList.Application.Helpers
{
    public class ManageDefectStatusCommand : IManageDefectStatusCommand
    {
        private readonly IAssignedDefectRepository _assignedDefectRepository;
        private readonly IDefectStatusHandler _defectStatusHandler;
        public ManageDefectStatusCommand(IAssignedDefectRepository assignedDefectRepository,
            IDefectStatusHandler defectStatusHandler)
        {
            _assignedDefectRepository = assignedDefectRepository;
            _defectStatusHandler = defectStatusHandler;
        }
        public async Task SendKeyboardWithDefects(int telegramId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(GetDefectsKeyboard(telegramId));
            await TelegramBotEntity.Bot.SendTextMessageAsync(
                    chatId: telegramId,
                    text: "Choose",
                    replyMarkup: inlineKeyboard
                );
            TelegramBotEntity.Bot.OnCallbackQuery += Bot_OnCallbackQuery;
        }

        private async void Bot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            var callbackQuery = e.CallbackQuery;

            await _defectStatusHandler.SendKeyboardWithStatuses(e.CallbackQuery.From.Id, Convert.ToInt32(callbackQuery.Data));
            TelegramBotEntity.Bot.OnCallbackQuery -= Bot_OnCallbackQuery;
        }

        private IEnumerable<InlineKeyboardButton> GetDefectsKeyboard(int telegramId)
        {
            var defects = _assignedDefectRepository
                .GetAssignedDefectsByUserId(telegramId)
                .ToArray();

            InlineKeyboardButton[] inlineKeyboardButtons = new InlineKeyboardButton[defects.Length];

            for (int i = 0; i < defects.Length; i++)
            {
                inlineKeyboardButtons[i] = InlineKeyboardButton
                    .WithCallbackData(defects[i].Defect.Description, defects[i].Id.ToString());
                
            }
            return inlineKeyboardButtons;
        }
    }
}
