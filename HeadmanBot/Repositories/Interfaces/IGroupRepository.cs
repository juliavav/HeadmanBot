using System.Threading.Tasks;
using HeadmanBot.Data.Entities;
using HeadmanBot.Data.Models;

namespace HeadmanBot.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task AddAsync(Group group);
        Task<Group> GetAsync(long telegramId);
        Task<bool> IsExist(long telegramId);
    }
}