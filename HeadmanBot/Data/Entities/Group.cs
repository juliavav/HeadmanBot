using System.Collections.Generic;

namespace HeadmanBot.Data.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public string Name { get; set; }

        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
    }
}