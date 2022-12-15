namespace DanceAcademy.Controllers
{
	using DanceAcademy.Models;
	using JustDanceAcademy.Models;
	using JustDanceAcademy.Web.Controllers;
	using JustDanceAcademy.Web.ViewModels.Models;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System.Diagnostics;
	using System;

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

		//		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		//		public IActionResult Error(string message)

		//{
		//			Console.WriteLine();

		//			return this.View(
		//				new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
		//		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error(int statusCode)
		{
			if (statusCode == 400)
			{
				return this.View("Error400");
			}

			if (statusCode == 401)
			{
				return this.View("Error401");
			}

			if (statusCode == 404)
			{
				return this.View("Error404");
			}

			return this.View();
		}
	}
}
