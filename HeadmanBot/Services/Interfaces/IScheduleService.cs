using System.Collections.Generic;
using System.Threading.Tasks;
using HeadmanBot.Data.Models;

namespace HeadmanBot.Services.Interfaces
{
    public interface IScheduleService
    {
        List<SubjectModel> DeserializeScheduleAsync(string json);
        Task UpdateScheduleAsync(List<SubjectModel> subjectModels, long groupId);
    }
}