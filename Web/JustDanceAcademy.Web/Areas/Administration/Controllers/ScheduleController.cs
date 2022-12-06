namespace JustDanceAcademy.Web.Areas.Administration.Controllers
{
	using System;
	using System.Threading.Tasks;

	using JustDanceAcademy.Services.Data.Constants;
	using JustDanceAcademy.Web.ViewModels.Models;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Area("Administration")]

	[Route("Administration/[controller]/[Action]/{id?}")]

	[Authorize(Roles = "Administrator")]
	public class ScheduleController : AdministrationController
	{
		private readonly IScheduleService scheduleService;
		private readonly ILevelCategoryService levelCategoryService;

		public ScheduleController(IScheduleService scheduleService, ILevelCategoryService levelCategoryService)
		{
			this.scheduleService = scheduleService;
			this.levelCategoryService = levelCategoryService;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var model = await this.scheduleService.AllSchedules();
			return this.View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var model = new AddScheduleViewModel()
			{
				AllClasses = await this.scheduleService.GetClasses(),
			};
			return this.View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddScheduleViewModel model)
		{

			if (!this.ModelState.IsValid)
			{
				model.AllClasses = await this.scheduleService.GetClasses();
				return this.View(model);
			}


			try
			{
				await this.scheduleService.CreateSchedule(model);

				return this.RedirectToAction("Index", "Admin", new
				{
					area = "Administration",
				});

				// return this.RedirectToAction("SeeAll", "Schedule");
			}
			catch (Exception)
			{
				model.AllClasses = await this.scheduleService.GetClasses();

				this.ModelState.AddModelError(" ", "Something went wrong");

				return this.View(model);
			}
		}


		[HttpPost]

		public async Task<IActionResult> DeleteColumn(int id)
		{

			await this.scheduleService.DeleteColumn(id);

			return this.RedirectToAction(nameof(this.All));
		}
	}
}
