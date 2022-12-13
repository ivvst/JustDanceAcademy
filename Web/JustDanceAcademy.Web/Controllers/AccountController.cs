namespace JustDanceAcademy.Web.Controllers
{
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Models;
	using JustDanceAcademy.Services.Data.Common;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;

	public class AccountController : BaseController
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(
					SignInManager<ApplicationUser> _signInManager,
					UserManager<ApplicationUser> _userManager)
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
				if (this.User.IsInRole("Administrator"))
				{
					return this.RedirectToAction("Index", "Admin", new
					{
						area = "Administration",
					});
				}

				return this.RedirectToAction("Classes", "Class");
			}

			var model = new RegisterViewModel();
			return this.View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!this.ModelState.IsValid)
			{
				this.TempData["Msg"] = ExceptionMessages.RegisterError;

				return this.View(model);
			}

			var user = new ApplicationUser()
			{
				UserName = model.UserName,
				Email = model.Email,
			};
			var exists = await this.userManager.FindByNameAsync(model.UserName);
			var emailExist = await this.userManager.FindByEmailAsync(model.Email);
			if (exists != null)
			{
				this.TempData["Msg"] = ExceptionMessages.UserNameTaken;
				return this.View(model);
			}

			if (emailExist != null)
			{
				this.TempData["Msg"] = ExceptionMessages.EmailTaken;
				return this.View(model);
			}

			var result = await this.userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				return this.RedirectToAction("Login", "Account");
			}

			foreach (var item in result.Errors)
			{
				this.ModelState.AddModelError(string.Empty, item.Description);
			}

			this.TempData["Msg"] = ExceptionMessages.RegisterError;

			return this.View(model);
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login()
		{
			if (this.User?.Identity?.IsAuthenticated ?? false)
			{
				if (this.User.IsInRole("Administrator"))
				{
					return this.RedirectToAction("Index", "Admin", new
					{
						area = "Administration",
					});
				}

				return this.RedirectToAction("Classes", "Class");
			}

			var model = new LoginViewModel();

			return this.View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!this.ModelState.IsValid)
			{
				this.TempData["Msg"] = ExceptionMessages.LoginError;

				return this.View(model);
			}

			var user = await this.userManager.FindByNameAsync(model.UserName);

			if (user != null)
			{
				var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

				if (result.Succeeded)
				{
					var student = await this.userManager.FindByNameAsync(model.UserName);

					if (student != null && await this.userManager.IsInRoleAsync(student, "Administrator"))
					{
						return this.RedirectToAction("Index", "Admin", new
						{
							area = "Administration",
						});
					}

					return this.RedirectToAction("Classes", "Class");
				}
				else if (result.Succeeded == false)
				{
					this.TempData["Msg"] = ExceptionMessages.LoginError;

					// ModelState.AddModelError(" ", "Something went wrong");
				}
			}

			this.TempData["Msg"] = ExceptionMessages.LoginError;
			return this.View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await this.signInManager.SignOutAsync();

			return this.RedirectToAction("Index", "Home");
		}
	}
}
