namespace JustDanceAcademy.Services.Data.Constants
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Models;
	using JustDanceAcademy.Web.ViewModels.Models;

	public interface IDanceClassService
	{
		Task<IEnumerable<Class>> GetAllAsync();

		Task<int> GetCountAsync();

		Task<int> CreateClassAsync(AddClassViewModel model);

		Task<int> GetDanceLevelId(int classId);

		Task<ClassesViewModel> DanceDetailsById(int id);

		Task<bool> Exists(int id);

		Task Edit(int classId, EditDanceViewModel model);

		Task AddStudentToClass(string userId, int classId);

		Task<IEnumerable<MyClassViewModel>> GetMyClassAsync(string userId);

		Task<ClassStudent> LeaveClass(string userId);

		Task<int> CreatePlan(PlanViewModel model);

		Task<IEnumerable<PlanViewModel>> GetAllPlans();

		Task<ClassQueryModel> All(
			string category = null,
			string searchTerm = null,
			int currentPage = 1,
			int classPerPage = 1);

		Task<MemberShip> DeletePlan(int planId);
		Task<IEnumerable<string>> AllCategoriesNames();

		Task<IEnumerable<Review>> AllReviews();

		Task<int> CreateReview(int classId, string userId, ReviewViewModel model);


		Task<bool> DoesUserHaveClass(string userId);

		Task<Class> GetClassForReview(string classId);

		Task<bool> PhoneNotifyForClass(string userId);

		Task<ApplicationUser> TakeNumberForStart(string userId, int planId);

		Task<Class> DeleteClass(int classId);

		Task<Review> DeleteReview(int reviewId);

		Task<int> GetStaticticsTakenPlans(int id);
	}
}
