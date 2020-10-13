using HeadmanBot.Services.Interfaces;
using Microsoft.Extensions.Options;
using MihaZupan;
using Telegram.Bot;

namespace HeadmanBot.Services.Implementations
{
    public class BotService : IBotService
    {
        private readonly BotConfiguration config;

        public BotService(IOptions<BotConfiguration> config)
        {
            this.config = config.Value;
            Client = string.IsNullOrEmpty(this.config.Socks5Host)
                ? new TelegramBotClient(this.config.BotToken)
                : new TelegramBotClient(
                    this.config.BotToken,
                    new HttpToSocks5Proxy(this.config.Socks5Host, this.config.Socks5Port));
        }

        public TelegramBotClient Client { get; }
    }
}