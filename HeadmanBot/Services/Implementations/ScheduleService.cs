using System.Collections.Generic;
using System.Threading.Tasks;
using HeadmanBot.Data.Models;
using HeadmanBot.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace HeadmanBot.Services.Implementations
{
    public class ScheduleService : IScheduleService
    {
        public List<SubjectModel> DeserializeScheduleAsync(string json)
        {
            var jObject = JObject.Parse(json);
            var subjectsList = new List<SubjectModel>();
            
            foreach (var subjectToken in jObject["subjects"].Children())
            {
                var subjectModel = subjectToken.ToObject<SubjectModel>();
                subjectModel.Timetable = new List<TimetableModel>();
                
                foreach (var timetableToken in subjectToken["timetable"].Children())
                {
                    subjectModel.Timetable.Add(timetableToken.ToObject<TimetableModel>());    
                }
                subjectsList.Add(subjectModel);
            }

            return subjectsList;
        }

        public Task UpdateScheduleAsync(List<SubjectModel> subjectModels, long groupId)
        {
            throw new System.NotImplementedException();
        }
    }
}