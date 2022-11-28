namespace DanceAcademy.Controllers
{
	using DanceAcademy.Models;
	using JustDanceAcademy.Web.Controllers;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	public class HomeController : BaseController
	{
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Index()
		{

			var model = new HomeViewModel();
			return this.View(model);

		}

		[HttpGet]
		public IActionResult About()
		{
			var model = new AboutViewModel();
			return this.View(model);
		}


	}
}