namespace JustDanceAcademy.Web.Controllers
{
	using System.Threading.Tasks;

	using JustDanceAcademy.Services.Data.Constants;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	public class FAQController : Controller
	{
		private readonly IFaQService questService;

		public FAQController(IFaQService questService)
		{
			this.questService = questService;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Questions()
		{
			var model = await this.questService.GetAllAsync();

			return this.View(model);
		}
	}
}
