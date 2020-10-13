using System.Collections.Generic;

namespace HeadmanBot.Data.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }

        private ICollection<Subject> Subjects { get; set; }
    }
}