using System;
using HeadmanBot.Helpers;

namespace HeadmanBot.Data.Entities
{
    public class Timetable
    {
        public int Id { get; set; }
        public string TimeString { get; set; }
        public Constants.WeekType? WeekType { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string Room { get; set; }

        public Subject Subject { get; set; }
    }
}