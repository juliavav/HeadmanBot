using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HeadmanBot.Data.Entities;
using HeadmanBot.Data.Models;
using HeadmanBot.Helpers;
using HeadmanBot.Repositories.Interfaces;
using HeadmanBot.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace HeadmanBot.Services.Implementations
{
    public class ScheduleService : IScheduleService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IMapper mapper;

        public ScheduleService(IGroupRepository groupRepository, IMapper mapper)
        {
            this.groupRepository = groupRepository;
            this.mapper = mapper;
        }
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

        public async Task UpdateScheduleAsync(List<SubjectModel> subjectModels, long groupId)
        {
            var group = await groupRepository.IsExist(groupId)
                ? await groupRepository.GetAsync(groupId)
                : new Group {TelegramId = groupId};

            foreach (var subjectModel in subjectModels)
            {
                //var model = mapper.Map<IList<CertificationModel>>(certifications);
                var currentSubject = new Subject
                {
                    Name = subjectModel.Name,
                    Teacher = subjectModel.Teacher,
                    Description = subjectModel.Description,
                    Group = group
                };
                
                foreach (var model in subjectModel.Timetable)
                {
                    currentSubject.Timetables.Add(
                        new Timetable
                        {
                            DayOfWeek = model.DayOfWeek, 
                            Room = model.Room, 
                            TimeString = model.Time, 
                            WeekType = model.Week ?? Constants.WeekType.Both
                        });
                }
                
                
                group.Subjects.Add(currentSubject);
            }

        }
    }
}