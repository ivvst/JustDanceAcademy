using DanceAcademy.Models;
using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
using JustDanceAcademy.Services.Data;
using JustDanceAcademy.Services.Data.Common;
using JustDanceAcademy.Services.Data.Constants;
using JustDanceAcademy.Services.Messaging.Constants;
using JustDanceAcademy.Web.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.Controllers
{
	[Authorize]
	public class ClassController : BaseController

	{
		private readonly IDanceClassService danceService;
		private readonly ILevelCategoryService levelCategoryService;

		public ClassController(
			ILevelCategoryService levelCategoryService
			, IDanceClassService danceService)
		{
			this.levelCategoryService = levelCategoryService;
			this.danceService = danceService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery] AllClassesQueryModel query)
		{

			var result = await danceService.All(
				query.Category,
				query.SearchTerm,
				query.CurrentPage,
				AllClassesQueryModel.ClassesPerPage);

			query.TotalClassesCount = result.TotalClassesCount;
			query.LevelsCategory = await danceService.AllCategoriesNames();
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
			var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			if ((await this.danceService.DoesUserHaveClass(userId)) == true || this.User.IsInRole("Administrator"))
			{

				var model = await this.danceService.GetAllPlans();
				return this.View(model);
			}
			return RedirectToAction(nameof(this.Classes));


		}


		
		[HttpGet]
		public async Task<IActionResult> Classes()
		{
			var model = await this.danceService.GetAllAsync();

			return this.View(model);
		}


		//When user is not administrator let's not See the button 

		public async Task<IActionResult> StartClass(int classId)
		{

			var userId = this.User.Claims
				.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;


			if (this.User.IsInRole("Administrator"))
			{
				throw new NullReferenceException(string.Format(ExceptionMessages.AdminHaveNotClass, userId));


			}

			if (await this.danceService.DoesUserHaveClass(userId) == true)
			{
				return RedirectToAction(nameof(this.Train));
			}


			await danceService.AddStudentToClass(userId, classId);

			return this.RedirectToAction(nameof(this.Plans));



		}

		public async Task<IActionResult> GetNumber()
		{
			var userId = this.User.Claims
					.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
			if (await this.danceService.PhoneNotifyForClass(userId) == true)
			{
				TempData["mssg"] = "Succesfully Added";
				return this.RedirectToAction(nameof(this.Train));
			}

			await this.danceService.TakeNumberForStart(userId);
			return this.RedirectToAction(nameof(this.Train));
		}

		public async Task<IActionResult> LeaveClass(int classId)
		{


			var userId = this.User.Claims
						.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
			await danceService.LeaveClass(classId, userId);

			return this.RedirectToAction(nameof(this.Classes));


		}


		public async Task<IActionResult> Train()
		{


			var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			if (this.User.IsInRole("Administrator"))
			{
				return RedirectToAction(nameof(this.Classes));
			}

			var model = await this.danceService.GetMyClassAsync(userId);

			this.ViewBag.mssg = this.TempData["mssg"] as string;


			return View("Training", model);


		}



		[HttpGet]
		public async Task<IActionResult> Write()
		{
			var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

			if (this.User.IsInRole("Administrator"))
			{
				return RedirectToAction(nameof(this.Classes));
			}
			//var model = await this.danceService.GetMyClassAsync(userId);
			//return View("Training", model);
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
			var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			if (ModelState.IsValid == false)
			{
				model.NameClass = await this.danceService.GetClassForReview(userId);
				return View(model);

			}

			await this.danceService.CreateReview(userId, model.ClassId, model);

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
