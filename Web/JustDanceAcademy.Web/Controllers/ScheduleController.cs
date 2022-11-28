﻿namespace JustDanceAcademy.Web.Controllers
{
	using System.Threading.Tasks;

	using JustDanceAcademy.Services.Data.Constants;
	using Microsoft.AspNetCore.Mvc;

	public class ScheduleController : BaseController
	{
		private readonly IScheduleService scheduleService;

		public ScheduleController(IScheduleService scheduleService)
		{
			this.scheduleService = scheduleService;
		}

		[HttpGet]
		public async Task<IActionResult> SeeAll()
		{
			if (this.User.IsInRole("Administrator"))
			{
				return this.RedirectToAction("All", "Schedule", new
				{
					area = "Administration",
				});
			}

			var model = await this.scheduleService.AllSchedules();
			return this.View(model);
		}
	}
}
