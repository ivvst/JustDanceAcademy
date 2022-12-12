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
	public class InstructorController : AdministrationController
	{
		private readonly IServiceInstructor serviceInstructor;
		private readonly IDanceClassService danceService;

		public InstructorController(IServiceInstructor serviceInstructor, IDanceClassService danceService)
		{
			this.serviceInstructor = serviceInstructor;
			this.danceService = danceService;
		}

		[HttpGet]
		public async Task<IActionResult> ViewTrainers()
		{
			var model = await this.serviceInstructor.GetAllInstructors();
			return this.View(model);
		}


		[HttpGet]
		[Authorize(Roles = "Administrator")]

		public async Task<IActionResult> New()
		{
			var model = new InstructorViewModel()
			{
				ClassesOfInstructor = await this.serviceInstructor.GetClasses(),
			};
			return this.View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrator")]

		public async Task<IActionResult> New(InstructorViewModel model)
		{
			if (await this.serviceInstructor.DoesInstructorExist(model.FullName) == true)
			{
				model.ClassesOfInstructor = await this.serviceInstructor.GetClasses();

				this.TempData["Msg"] = ExceptionMessages.InstructorAlreadyExists;

				return this.View(model);

			}



			if (!this.ModelState.IsValid)
			{
				model.ClassesOfInstructor = await this.serviceInstructor.GetClasses();
				return this.View(model);
			}

			try
			{
				await this.serviceInstructor.AddInstructor(model);


				return this.RedirectToAction("ViewTrainers", "Instructor");
			}
			catch (Exception)
			{
				model.ClassesOfInstructor = await this.serviceInstructor.GetClasses();

				this.ModelState.AddModelError("", "Something went wrong");

				return this.View(model);
			}

		}

		[HttpGet]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Edit(int id)
		{
			if ((await this.serviceInstructor.Exists(id)) == false)
			{
				throw new NullReferenceException(
					string.Format(ExceptionMessages.InstructorNotFound));

			}

			var trainer = await this.serviceInstructor.TrainerDetailsById(id);
			var danceClassId = await this.serviceInstructor.GetClassId(id);

			var model = new InstructorViewModel()
			{
				Id = id,
				FullName = trainer.FullName,
				ImageUrl = trainer.ImageUrl,
				AboutYou = trainer.Intro,
				ClassesOfInstructor = await this.serviceInstructor.GetClasses(),
				ClassId = danceClassId,
			};

			return this.View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Edit(int id, InstructorViewModel model)
		{
			if (this.ModelState.IsValid == false)
			{
				model.ClassesOfInstructor = await this.serviceInstructor.GetClasses();
				return this.View(model);
			}

			await this.serviceInstructor.Edit(model.Id, model);

			return this.RedirectToAction("ViewTrainers", "Instructor", new
			{
				area = "Administration",
			});
		}

		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await this.serviceInstructor.DeleteInstructor(id);
				this.TempData["Msg"] = OperationalMessages.DeletedTrainer;
				return this.RedirectToAction(nameof(this.ViewTrainers));

			}
			catch (Exception)
			{
				this.TempData["Msg"] = ExceptionMessages.TrainerHasClass;
				return this.RedirectToAction(nameof(this.ViewTrainers));

			}
		}

		[HttpPost]
		public async Task<IActionResult> ShowAllClasses(int classId)
		{
          var result = await this.serviceInstructor.GetClassWithAllCategoriesView(classId);

			this.ViewData["obj"] = result;
			return this.View(this.ViewData);
		}
	}
}
