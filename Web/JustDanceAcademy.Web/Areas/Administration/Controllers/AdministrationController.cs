namespace JustDanceAcademy.Web.Areas.Administration.Controllers
{
	using JustDanceAcademy.Web.Controllers;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Area("Administration")]
	[Route("Administration/[controller]/[Action]/{id?}")]
	[Authorize(Roles = "Administrator")]
	public class AdministrationController : BaseController
	{
	}
}
