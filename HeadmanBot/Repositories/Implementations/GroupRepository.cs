using System;
using System.Threading.Tasks;
using HeadmanBot.Data;
using HeadmanBot.Data.Entities;
using HeadmanBot.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeadmanBot.Repositories.Implementations
{
    public class GroupRepository : IGroupRepository
    {
        private readonly Func<DataContext> factory;

        public GroupRepository(Func<DataContext> factory)
        {
            this.factory = factory;
        }
        
        public async Task AddAsync(Group group)
        {
            factory().Groups.Add(group);
            await factory().SaveChangesAsync();
        }

        public Task<Group> GetAsync(long telegramId)
        {
            return factory().Groups.SingleOrDefaultAsync(x=>x.TelegramId == telegramId);
        }

        public Task<bool> IsExist(long telegramId)
        {
            return factory().Groups.AnyAsync(x=>x.TelegramId == telegramId);
        }
    }
}