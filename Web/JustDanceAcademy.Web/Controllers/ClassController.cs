namespace JustDanceAcademy.Web.Controllers
{
	using System;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;

	using JustDanceAcademy.Services.Data.Common;
	using JustDanceAcademy.Services.Data.Constants;
	using JustDanceAcademy.Web.ViewModels.Models;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ClassController : BaseController

	{
		private readonly IDanceClassService danceService;
		private readonly ILevelCategoryService levelCategoryService;

		public ClassController(
			ILevelCategoryService levelCategoryService,
			IDanceClassService danceService)
		{
			this.levelCategoryService = levelCategoryService;
			this.danceService = danceService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery] AllClassesQueryModel query)
		{

			var result = await this.danceService.All(
				query.Category,
				query.SearchTerm,
				query.CurrentPage,
				AllClassesQueryModel.ClassesPerPage);

			query.TotalClassesCount = result.TotalClassesCount;
			query.LevelsCategory = await this.danceService.AllCategoriesNames();
			query.Classes = result.Classes;

			return this.View(query);
		}

		[HttpGet]
		public IActionResult Schedule()
		{
			var model = new ScheduleViewModel();

			return this.View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Plans()
		{
			var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			if ((await this.danceService.DoesUserHaveClass(userId)) == true || this.User.IsInRole("Administrator"))
			{
				if ((await this.danceService.PhoneNotifyForClass(userId)) == true)
				{
					this.TempData["Alert"] = ExceptionMessages.UserAlreadyPaid;

					return this.RedirectToAction(nameof(this.Train));
				}

				var model = await this.danceService.GetAllPlans();
				return this.View(model);
			}

			return this.RedirectToAction(nameof(this.Classes));
		}

		[HttpGet]
		public async Task<IActionResult> Classes()
		{
			var model = await this.danceService.GetAllAsync();

			return this.View(model);
		}


		// When user is  administrator let's not See the button 
		public async Task<IActionResult> StartClass(int classId)
		{

			var userId = this.User.Claims
				.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;


			if (this.User.IsInRole("Administrator"))
			{
				this.TempData["Msg"] = ExceptionMessages.AdminHaveNotClass;
			}

			if (await this.danceService.DoesUserHaveClass(userId) == true)
			{
				this.TempData["Msg"] = ExceptionMessages.ClassAlreadyIsStarted;

				return this.RedirectToAction(nameof(this.Classes));
			}

			await this.danceService.AddStudentToClass(userId, classId);

			this.TempData["Msg"] = ExceptionMessages.StartClass;

			return this.RedirectToAction(nameof(this.Train));
		}

		public async Task<IActionResult> GetNumber()
		{
			var userId = this.User.Claims
					.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
			if (await this.danceService.PhoneNotifyForClass(userId) == true)
			{
				this.TempData["Msg"] = ExceptionMessages.UserAlreadyPaid;
				return this.RedirectToAction(nameof(this.Train));
			}

			await this.danceService.TakeNumberForStart(userId);
			return this.RedirectToAction(nameof(this.Train));
		}

		public async Task<IActionResult> LeaveClass()
		{
			var userId = this.User.Claims
						.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
			await this.danceService.LeaveClass(userId);

			return this.RedirectToAction(nameof(this.Classes));
		}

		public async Task<IActionResult> Train()
		{
			var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			if (this.User.IsInRole("Administrator"))
			{
				return this.RedirectToAction(nameof(this.Classes));
			}

			var model = await this.danceService.GetMyClassAsync(userId);


			return this.View("Training", model);
		}

		[HttpGet]
		public async Task<IActionResult> Write()
		{
			var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

			if (this.User.IsInRole("Administrator"))
			{
				return this.RedirectToAction(nameof(this.Classes));
			}

			// var model = await this.danceService.GetMyClassAsync(userId);
			// return View("Training", model);
			if ((await this.danceService.DoesUserHaveClass(userId)) == false)
			{
				throw new NullReferenceException(string.Format(ExceptionMessages.StudentNotFound, userId));
			}


			var danceClass = await this.danceService.GetClassForReview(userId);

			var model = new ReviewViewModel()
			{
				NameClass = danceClass,
			};
			return this.View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Write(string studentId, int classId, ReviewViewModel model)
		{
			var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			if (this.ModelState.IsValid == false)
			{
				model.NameClass = await this.danceService.GetClassForReview(userId);
				return this.View(model);
			}

			await this.danceService.CreateReview(model.ClassId, userId, model);

			return this.RedirectToAction(nameof(this.Train));
		}

		[HttpGet]
		public async Task<IActionResult> Reviews()
		{
			var model = await this.danceService.AllReviews();
			return this.View(model);
		}
	}
}
