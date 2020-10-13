using System;

namespace HeadmanBot.Data.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string TimeString { get; set; }
        public WeekType WeekType { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string Room { get; set; }
        public string HomeWork { get; set; }

        public Subject Subject { get; set; }
    }

    public enum WeekType
    {
        Upper,
        Lower
    }
}