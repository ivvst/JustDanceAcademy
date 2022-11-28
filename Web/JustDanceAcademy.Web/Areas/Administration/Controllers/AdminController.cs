﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JustDanceAcademy.Web.Areas.Administration.Controllers
{
	using JustDanceAcademy.Common;
	using JustDanceAcademy.Web.Controllers;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Area("Administration")]
	[Route("Administration/[controller]/[Action]/{id?}")]
	[Authorize(Roles = "Administrator")]
	public class AdminController : AdministrationController
	{

		public IActionResult Index()
		{
			return this.View();
		}
	}
}




