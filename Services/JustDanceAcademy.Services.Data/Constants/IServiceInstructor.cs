using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
using JustDanceAcademy.Web.ViewModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustDanceAcademy.Services.Data.Constants
{
	public interface IServiceInstructor
	{
        Task<IEnumerable<InstructorsViewModel>> GetAllInstructors();

        Task<int> AddInstructor(InstructorViewModel model);
        Task<InstructorsViewModel> TrainerDetailsById(int id);

        Task<int> GetClassId(int trainerId);
        Task<bool> Exists(int id);
        Task Edit(int trainerId, InstructorViewModel model);

        Task<IEnumerable<Class>> GetClasses();

    }
}