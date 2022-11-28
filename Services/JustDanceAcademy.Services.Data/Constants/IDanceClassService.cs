﻿namespace JustDanceAcademy.Services.Data.Constants
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using JustDanceAcademy.Models;
	using JustDanceAcademy.Web.ViewModels.Models;

	public interface IDanceClassService
	{
		Task<IEnumerable<ClassesViewModel>> GetAllAsync();

		Task<int> CreateClassAsync(AddClassViewModel model);

		Task<int> GetDanceLevelId(int classId);

		Task<ClassesViewModel> DanceDetailsById(int id);

		Task<bool> Exists(int id);

		Task Edit(int classId, EditDanceViewModel model);

		Task AddStudentToClass(string userId, int classId);

		Task<IEnumerable<MyClassViewModel>> GetMyClassAsync(string userId);

		Task LeaveClass(int classId, string userId);

		Task<int> CreatePlan(PlanViewModel model);

		Task<IEnumerable<PlanViewModel>> GetAllPlans();

		Task<ClassQueryModel> All(
			string category = null,
			string searchTerm = null,
			int currentPage = 1,
			int classPerPage = 1);

		Task<IEnumerable<string>> AllCategoriesNames();

		Task<IEnumerable<ReviewViewModel>> AllReviews();

		Task<int> CreateReview(string userId, int classId, ReviewViewModel model);

		Task<bool> DoesUserHaveClass(string userId);

		Task<string> GetClassForReview(string classId);

		Task<bool> PhoneNotifyForClass(string userId);

		Task TakeNumberForStart(string userId);

		Task DeleteClass(int classId);
	}
}
