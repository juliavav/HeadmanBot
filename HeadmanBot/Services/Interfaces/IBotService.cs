using Telegram.Bot;

namespace HeadmanBot.Services.Interfaces
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}