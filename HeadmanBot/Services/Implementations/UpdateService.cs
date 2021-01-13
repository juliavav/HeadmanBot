using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HeadmanBot.Data.Entities;
using HeadmanBot.Data.Models;
using HeadmanBot.Repositories.Interfaces;
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
        private readonly IGroupRepository groupRepository;
        private readonly IMapper mapper;

        public UpdateService(IBotService botService, IScheduleService scheduleService, ILogger<UpdateService> logger, 
            IGroupRepository groupRepository, IMapper mapper)
        {
            this.botService = botService;
            this.scheduleService = scheduleService;
            this.groupRepository = groupRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task ProcessMessageAsync(Update update)
        {
            if (update.Type != UpdateType.Message)
                return;

            var message = update.Message;


            switch (message.Type)
            {
                case MessageType.Text:
                    var args = message.Text.Split(' ');
                    switch (args.First())
                    {
                        case "/start":
                            var group = new Group
                            {
                                Name = update.Message.Chat.Title,
                                TelegramId = update.Message.Chat.Id
                            };
                            await groupRepository.AddAsync(group);
                            break;
                        case "/today":
                            break;
                        case "/tomorrow":
                            break;
                        case "/news":
                            if (args.Length > 1)
                            {
                                var text = message.Text.Substring(args.Length);
                                //
                            }
                            break;
                        case "/news-latest":
                            var newsCount = 10;
                            if (args.Length == 1)
                            {
                                // top 10
                            }
                            else
                            {
                                 if(!Int32.TryParse(args[1], out newsCount))
                                     newsCount=10;
                                 
                            }
                            break;
                    }

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