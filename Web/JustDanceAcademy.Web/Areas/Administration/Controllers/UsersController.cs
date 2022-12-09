namespace JustDanceAcademy.Web.Areas.Administration.Controllers
{
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Services.Data.Constants;
	using JustDanceAcademy.Web.Hubs;
	using JustDanceAcademy.Web.ViewModels.Administration.Users;
	using JustDanceAcademy.Web.ViewModels.Models;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.SignalR;
	using System.Threading.Tasks;

	public class UsersController : AdministrationController
	{
		private readonly IUsersService usersService;

		private readonly IHubContext<NotificationHub> notificationHub;

		public UsersController(IUsersService usersService, IHubContext<NotificationHub> notificationHub)
		{
			this.usersService = usersService;
			this.notificationHub = notificationHub;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return this.View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(NotificationViewModel model)
		{
			await this.notificationHub.Clients.All.SendAsync("ReceiveMsg", model.Message);
			return this.View();
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

