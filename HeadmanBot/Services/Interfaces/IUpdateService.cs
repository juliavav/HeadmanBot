using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace HeadmanBot.Services.Interfaces
{
    public interface IUpdateService
    {
        Task ProcessMessageAsync(Update update);
    }
}