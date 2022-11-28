namespace JustDanceAcademy.Web.Areas.Administration.Controllers
{
	using JustDanceAcademy.Services.Data.Constants;
	using JustDanceAcademy.Web.ViewModels.Administration.Users;
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	public class UsersController : AdministrationController
	{
		private readonly IUsersService usersService;

		public UsersController(IUsersService usersService)
		{
			this.usersService = usersService;
		}

		public async Task<IActionResult> All()
		{
			var model = new AllUsersViewModel
			{
				Users = await this.usersService.GetAllUsersAsync(),
			};

			return this.View(model);
		}
	}
}

