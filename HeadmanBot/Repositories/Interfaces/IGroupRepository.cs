using System.Threading.Tasks;
using HeadmanBot.Data.Entities;

namespace HeadmanBot.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task AddAsync(Group group);
        Task<Group> GetAsync(long telegramId);
        Task UpdateAsync(Group group);
        Task<bool> IsExist(long telegramId);
    }
}