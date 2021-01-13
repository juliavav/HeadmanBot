using System.Collections.Generic;

namespace HeadmanBot.Data.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Description { get; set; }

        public Group Group { get; set; }
        public ICollection<Timetable> Timetables { get; set; }
    }
}