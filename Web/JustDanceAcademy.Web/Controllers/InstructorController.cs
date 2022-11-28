namespace JustDanceAcademy.Web.Controllers
{
	using System.Threading.Tasks;

	using JustDanceAcademy.Services.Data.Constants;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class InstructorController : BaseController
	{
		private readonly IServiceInstructor serviceInstructor;
		private readonly IDanceClassService danceService;

		public InstructorController(IServiceInstructor serviceInstructor, IDanceClassService danceService)
		{
			this.serviceInstructor = serviceInstructor;
			this.danceService = danceService;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			if (this.User.IsInRole("Administrator"))
			{
				return this.RedirectToAction("Index", "Admin", new
				{
					area = "Administration",
				});
			}
			var model = await this.serviceInstructor.GetAllInstructors();
			return this.View(model);
		}
	}
}

// var model = new AddBookViewModel();
// var categories = await this.genreCategoryService.AllCategories();

// model.GenresCategory = categories.Select(x => new GenreCategory
//    {
//        Id = x.Id,
//        Name = x.Name,
//    });
//    return this.View(model);

// var model = new InstructorViewModel();
// var classes = await this.serviceInstructor.GetClasses();

// model.ClassesOfInstructor = classes.Select(x => new Class
// {
//    Id = x.Id,
//    Name = x.Name,
// });
// return this.View(model);