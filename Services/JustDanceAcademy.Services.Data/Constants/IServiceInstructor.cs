namespace JustDanceAcademy.Services.Data.Constants
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Web.ViewModels.Models;

	public interface IServiceInstructor
	{
		Task<IEnumerable<Instrustor>> GetAllInstructors();

		Task<int> AddInstructor(InstructorViewModel model);

		Task<bool> DoesInstructorExist(string name);

		Task<InstructorsViewModel> TrainerDetailsById(int id);

		Task<int> GetClassId(int trainerId);

		Task<bool> Exists(int id);

		Task Edit(int trainerId, InstructorViewModel model);

		Task<IEnumerable<Class>> GetClasses();

		Task<Instrustor> DeleteInstructor(int trainerId);

		Task<Dictionary<string, List<string>>> GetClassWithAllCategoriesView(int classId);
	}
}
