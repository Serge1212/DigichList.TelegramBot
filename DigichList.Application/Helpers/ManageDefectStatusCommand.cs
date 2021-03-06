using static DigichList.Application.Helpers.TelegramBotMessageSender;
using DigichList.Application.Configuration;
using DigichList.Application.Interfaces;
using DigichList.Core.Repositories;
using System;
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
            var defectsKeyboard = GetDefectsKeyboard(telegramId);
            
            if(!defectsKeyboard.Any() || defectsKeyboard.First() == null && defectsKeyboard.Count() == 1)
            {
                await TelegramBotEntity.Bot.SendTextMessageAsync(telegramId, "У вас немає призначених дефектів");
                return;
            }

            var inlineKeyboard = new InlineKeyboardMarkup(defectsKeyboard);
            await TelegramBotEntity.Bot.SendTextMessageAsync(
                    chatId: telegramId,
                    text: "Виберіть дефект для зміни статусу",
                    replyMarkup: inlineKeyboard
                );
            TelegramBotEntity.Bot.OnCallbackQuery += Bot_OnCallbackQuery;

            async void Bot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
            {
                Console.WriteLine(telegramId);
                if (telegramId != e.CallbackQuery.From.Id) return;
                if (telegramId != e.CallbackQuery.From.Id) return;
                
                var callbackQuery = e.CallbackQuery;

                if (callbackQuery.Data == null)
                {
                    await SendMessageAsync
                        (
                        telegramId,
                        "Сталася помилка при виборі дефекту. Спробуйте ввести команду \"/setdefectstatus\" знову"
                        );

                    return;
                }


                await _defectStatusHandler.SendKeyboardWithStatuses(e.CallbackQuery.From.Id, Convert.ToInt32(callbackQuery.Data));
                TelegramBotEntity.Bot.OnCallbackQuery -= Bot_OnCallbackQuery;
            }
        }

        /// <summary>
        /// A method that generates assigned defects keyboard
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns>Returns a telegram keyboard with assigned defects </returns>
        private IEnumerable<InlineKeyboardButton> GetDefectsKeyboard(int telegramId)
        {
            var defects = _assignedDefectRepository
                .GetAssignedDefectsByUserId(telegramId)
                .ToArray();

            InlineKeyboardButton[] inlineKeyboardButtons = new InlineKeyboardButton[defects.Length];

            for (int i = 0; i < defects.Length; i++)
            {
                if (defects[i].ClosedAt == null)
                {
                    inlineKeyboardButtons[i] = InlineKeyboardButton
                        .WithCallbackData(defects[i].Defect.Description, defects[i].DefectId.ToString());
                }
                
            }

            return inlineKeyboardButtons;
        }
    }
}
