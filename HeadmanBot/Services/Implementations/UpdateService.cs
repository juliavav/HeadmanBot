using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HeadmanBot.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using File = System.IO.File;

namespace HeadmanBot.Services.Implementations
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService botService;
        private readonly IScheduleService scheduleService;

        public UpdateService(IBotService botService, IScheduleService scheduleService)
        {
            this.botService = botService;
            this.scheduleService = scheduleService;
        }

        public async Task EchoAsync(Update update)
        {
            if (update.Type != UpdateType.Message)
                return;

            var message = update.Message;


            switch (message.Type)
            {
                case MessageType.Text:
                    // Echo each Message
                    await botService.Client.SendTextMessageAsync(message.Chat.Id, message.Text);
                    break;

                case MessageType.Document:
                    await botService.Client.SendTextMessageAsync(message.Chat.Id, "doc");
                    break;
            }
        }
    }
}