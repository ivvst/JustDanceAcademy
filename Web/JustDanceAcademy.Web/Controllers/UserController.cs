namespace JustDanceAcademy.Controllers
{
	using JustDanceAcademy.Models;
	using Microsoft.AspNetCore.Mvc;

	public class UserController : Controller
	{
		public IActionResult About()
		{
			var model = new HomeViewModel();
			return this.View(model);
		}
	}
}
