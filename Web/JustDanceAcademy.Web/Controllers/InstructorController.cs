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

namespace JustDanceAcademy.Web.Controllers
{
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
        public  async Task<IActionResult> All()
        {
            var model = await this.serviceInstructor.GetAllInstructors();
            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            var model = new InstructorViewModel()
            {
                ClassesOfInstructor = await this.serviceInstructor.GetClasses(),
            };
            return this.View(model);
        }

        [HttpPost]
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