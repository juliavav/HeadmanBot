using System;
using HeadmanBot.Helpers;

namespace HeadmanBot.Data.Models
{
    public class TimetableModel
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string Time { get; set; }
        public string Room { get; set; }
        public Constants.WeekType? Week { get; set; }
    }
}