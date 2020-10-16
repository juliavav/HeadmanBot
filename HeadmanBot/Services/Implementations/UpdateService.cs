using System;
using System.IO;
using System.Threading.Tasks;
using HeadmanBot.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HeadmanBot.Services.Implementations
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService botService;
        private readonly IScheduleService scheduleService;
        private readonly ILogger<UpdateService> logger;

        public UpdateService(IBotService botService, IScheduleService scheduleService, ILogger<UpdateService> logger)
        {
            this.botService = botService;
            this.scheduleService = scheduleService;
            this.logger = logger;
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
                    
                    try
                    {
                        await using var memoryStream = new MemoryStream();
                        var file = await botService.Client.GetFileAsync(update.Message.Document.FileId);
                        await botService.Client.DownloadFileAsync(file.FilePath, memoryStream);
                        using TextReader textReader = new StreamReader(memoryStream);
                        var json = textReader.ReadToEnd();

                        var subjectModels = scheduleService.DeserializeScheduleAsync(json);
                        await scheduleService.UpdateScheduleAsync(subjectModels, message.Chat.Id);
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e.Message);
                        await botService.Client.SendTextMessageAsync(message.Chat.Id, "Некорректный файл. Невозможно обновить расписание");
                    }
                    break;
            }
        }
    }
}