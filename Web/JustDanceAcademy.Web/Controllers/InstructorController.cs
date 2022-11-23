using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
using JustDanceAcademy.Services.Data.Constants;
using JustDanceAcademy.Web.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using JustDanceAcademy.Services.Data.Common;

namespace JustDanceAcademy.Web.Controllers
{
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
            var model = await this.serviceInstructor.GetAllInstructors();
            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> New()
        {
            var model = new InstructorViewModel()
            {
                ClassesOfInstructor = await this.serviceInstructor.GetClasses(),
            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> New(InstructorViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.ClassesOfInstructor = await this.serviceInstructor.GetClasses();
                return this.View(model);
            }

            try
            {
                await this.serviceInstructor.AddInstructor(model);


                return this.RedirectToAction("All", "Instructor");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");

                return this.View(model);
            }

        }

        [HttpGet]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await this.serviceInstructor.Exists(id)) == false)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.InstructorNotFound, id));

            }

            var trainer = await this.serviceInstructor.TrainerDetailsById(id);
            var danceClassId = await this.serviceInstructor.GetClassId(id);

            var model = new InstructorViewModel()
            {
                Id = id,
                FullName = trainer.FullName,
                ImageUrl = trainer.ImageUrl,
                AboutYou = trainer.Intro,
                ClassesOfInstructor = await this.serviceInstructor.GetClasses(),
                ClassId = danceClassId,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, InstructorViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                model.ClassesOfInstructor = await this.serviceInstructor.GetClasses();
                return View(model);
            }

            await this.serviceInstructor.Edit(model.Id, model);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}

//var model = new AddBookViewModel();
//var categories = await this.genreCategoryService.AllCategories();

//model.GenresCategory = categories.Select(x => new GenreCategory
//    {
//        Id = x.Id,
//        Name = x.Name,
//    });
//    return this.View(model);

//var model = new InstructorViewModel();
//var classes = await this.serviceInstructor.GetClasses();

//model.ClassesOfInstructor = classes.Select(x => new Class
//{
//    Id = x.Id,
//    Name = x.Name,
//});
//return this.View(model);