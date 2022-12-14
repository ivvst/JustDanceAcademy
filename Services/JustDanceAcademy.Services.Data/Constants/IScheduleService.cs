using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Web.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JustDanceAcademy.Services.Data.Constants
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> AllSchedules();

        Task<int> CreateSchedule(AddScheduleViewModel model);

        Task<int> GetScheduleById(int id);
        Task<IEnumerable<Class>> GetClasses();

        Task<Schedule> DeleteColumn(int scheduleId);
    }
}
