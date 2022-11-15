using DanceAcademy.Models;
using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
using JustDanceAcademy.Services.Data;
using JustDanceAcademy.Services.Data.Constants;
using JustDanceAcademy.Web.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.Controllers
{
    [Authorize]
    public class ClassController : BaseController

    {
        private readonly IDanceClassService danceService;
        private readonly ILevelCategoryService levelCategoryService;

        public ClassController(
            ILevelCategoryService levelCategoryService
            , IDanceClassService danceService)
        {
            this.levelCategoryService = levelCategoryService;
            this.danceService = danceService;
        }



        [HttpGet]
        public async Task<IActionResult> Classes()
        {
            var model = await this.danceService.GetAllAsync();

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Add()
        {
            var model = new AddClassViewModel();
            var categories = await this.levelCategoryService.AllCategories();
            model.LevelsCategory = categories.Select(x => new LevelCategory
            {
                Name = x.Name,
                Id = x.Id,
            });
            return this.View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Add(AddClassViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await danceService.CreateClassAsync(model);

                return RedirectToAction("Classes", "Class");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
               
            }

        }
    }
}

//try
//{
//    await bookService.AddBookAsync(model);

//    return RedirectToAction(nameof(All));
//}
//catch (Exception)
//{
//    ModelState.AddModelError("", "Something went wrong");

//    return View(model);
//}