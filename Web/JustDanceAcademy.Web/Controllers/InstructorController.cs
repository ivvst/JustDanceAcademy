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
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await this.serviceInstructor.GetAllInstructors();
            return this.View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ShowCategories(int classId)
        {
            var result = await this.serviceInstructor.GetClassWithAllCategoriesView(classId);

            this.ViewData["obj"] = result;
            return this.View(this.ViewData);
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
// return this.View(model)
