namespace JustDanceAcademy.Web.Areas.Administration.Controllers
{
	using JustDanceAcademy.Web.ViewModels.Models;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System.Data;
	using System.Threading.Tasks;
	using System;
	using JustDanceAcademy.Services.Data.Constants;
	using JustDanceAcademy.Services.Data.Common;

	public class FAQController : AdminController
	{
		private readonly IFaQService questService;

		public FAQController(IFaQService questService)
		{
			this.questService = questService;
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public IActionResult Create()
		{
			var model = new QuestViewModel();

			return this.View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrator")]

		public async Task<IActionResult> Create(QuestViewModel model)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}
			if (await this.questService.IsQuestionAdded(model.Question) == true)
			{
				this.TempData["Msg"] = ExceptionMessages.QuestionAlreadyExists;
				return this.View(model);
			}

			try
			{
				await this.questService.CreateQuestWithAnswer(model);

				return this.RedirectToAction("All", "Users", new
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
	}
}


