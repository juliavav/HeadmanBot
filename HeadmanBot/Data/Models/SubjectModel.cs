using System;
using System.Collections.Generic;
using HeadmanBot.Data.Entities;

namespace HeadmanBot.Data.Models
{
    public class SubjectModel
    {
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Description { get; set; }
        public List<TimetableModel> Timetable { get; set; }
    }
}