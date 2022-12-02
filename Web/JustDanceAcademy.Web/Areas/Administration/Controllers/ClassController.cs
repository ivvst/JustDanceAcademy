namespace JustDanceAcademy.Web.Areas.Administration.Controllers
{
	using System;
	using System.Threading.Tasks;

	using JustDanceAcademy.Services.Data.Common;
	using JustDanceAcademy.Services.Data.Constants;
	using JustDanceAcademy.Web.ViewModels.Models;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Area("Administration")]
	[Route("Administration/[controller]/[Action]/{id?}")]
	[Authorize(Roles = "Administrator")]
	public class ClassController : AdministrationController
	{
		private readonly IDanceClassService danceService;
		private readonly ILevelCategoryService levelCategoryService;

		public ClassController(
		ILevelCategoryService levelCategoryService, IDanceClassService danceService)
		{
			this.levelCategoryService = levelCategoryService;
			this.danceService = danceService;
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public IActionResult Create()
		{
			var model = new PlanViewModel();

			return this.View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrator")]

		public async Task<IActionResult> Create(PlanViewModel model)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}

			try
			{
				await this.danceService.CreatePlan(model);

				return this.RedirectToAction("Index", "Admin", new
				{
					area = "Administration",
				});
			}
			catch (Exception)
			{
				this.ModelState.AddModelError(" ", "Something went wrong");
				return this.View(model);
			}
		}

		[HttpGet]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Add()
		{
			var model = new AddClassViewModel()
			{
				LevelsCategory = await this.levelCategoryService.AllCategories(),
			};

			return this.View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrator")]

		public async Task<IActionResult> Add(AddClassViewModel model)
		{
			if (!this.ModelState.IsValid)
			{
				model.LevelsCategory = await this.levelCategoryService.AllCategories();

				return this.View(model);
			}

			try
			{
				await this.danceService.CreateClassAsync(model);

				return this.RedirectToAction("Index", "Admin", new
				{
					area = "Administration",
				});
			}
			catch (Exception)
			{
				this.ModelState.AddModelError(" ", "Something went wrong");

				return this.View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> ViewClasses()
		{
			var model = await this.danceService.GetAllAsync();

			return this.View(model);
		}

		[HttpGet]

		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> EditDance(int id)
		{
			if ((await this.danceService.Exists(id)) == false)
			{
				return this.RedirectToAction("Index", "Admin", new
				{
					area = "Administration",
				});
			}

			var danceClass = await this.danceService.DanceDetailsById(id);
			var levelCategoryId = await this.danceService.GetDanceLevelId(id);

			var model = new EditDanceViewModel()
			{
				Id = id,
				Name = danceClass.Name,
				Instructor = danceClass.Instructor,
				Description = danceClass.Description,
				LevelCategoryId = levelCategoryId,
				ImageUrl = danceClass.ImageUrl,
				LevelsCategory = await this.levelCategoryService.AllCategories(),
			};
			return this.View(model);
		}

		[HttpPost]

		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> EditDance(int id, EditDanceViewModel model)
		{
			if ((await this.danceService.Exists(model.Id)) == false)
			{
				model.LevelsCategory = await this.levelCategoryService.AllCategories();
				throw new NullReferenceException(string.Format(ExceptionMessages.ClassDanceNotFound));
			}

			if (this.ModelState.IsValid == false)
			{
				model.LevelsCategory = await this.levelCategoryService.AllCategories();
				return this.View(model);
			}

			await this.danceService.Edit(model.Id, model);

			return this.RedirectToAction("Index", "Admin");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await this.danceService.DeleteClass(id);

			return this.RedirectToAction(nameof(this.ViewClasses));
		}
	}
}
