
using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
using JustDanceAcademy.Web.Areas.Administration.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.Controllers
{
	public class AccountController : BaseController
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;




		public AccountController(
			SignInManager<ApplicationUser> _signInManager,
			UserManager<ApplicationUser> _userManager
		   )
		{
			this.signInManager = _signInManager;
			this.userManager = _userManager;



		}

		[HttpGet]

		[AllowAnonymous]
		public IActionResult Register()
		{
			if (this.User?.Identity?.IsAuthenticated ?? false)
			{
				return this.RedirectToAction("Classes", "Class");
			}
			var model = new RegisterViewModel();
			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = new ApplicationUser()
			{
				UserName = model.UserName,
				Email = model.Email,

			};

			var result = await userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				return this.RedirectToAction("Login", "Account");
			}

			foreach (var item in result.Errors)
			{
				this.ModelState.AddModelError(string.Empty, item.Description);
			}

			return View(model);
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login()
		{
			if (this.User?.Identity?.IsAuthenticated ?? false)
			{
				return this.RedirectToAction("Classes", "Class");
			}

			var model = new LoginViewModel();

			return this.View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await this.userManager.FindByNameAsync(model.UserName);

			if (user != null)
			{
				var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

				if (result.Succeeded)
				{
					var student = await userManager.FindByNameAsync(model.UserName);

					if (student != null && await userManager.IsInRoleAsync(student, "Administrator"))
					{
						return this.RedirectToAction("Index", "Admin", new
						{
							area = "Administration",
						}
						);
					}
					return RedirectToAction("Classes", "Class");
				}
				else if (result.Succeeded == false)
				{
					ModelState.AddModelError(" ", "Something went wrong");
				}
			}

			ModelState.AddModelError("", "TRY AGAIN");

			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await this.signInManager.SignOutAsync();

			return this.RedirectToAction("Index", "Home");
		}
	}
}



