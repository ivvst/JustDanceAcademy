using DanceAcademy.Models;
using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
using JustDanceAcademy.Services.Data;
using JustDanceAcademy.Services.Data.Common;
using JustDanceAcademy.Services.Data.Constants;
using JustDanceAcademy.Services.Messaging.Constants;
using JustDanceAcademy.Web.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
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

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllClassesQueryModel query)
        {

            var result = await danceService.All(
                query.Category,
                query.SearchTerm,
                query.CurrentPage,
                AllClassesQueryModel.ClassesPerPage);

            query.TotalClassesCount = result.TotalClassesCount;
            query.LevelsCategory = await danceService.AllCategoriesNames();
            query.Classes = result.Classes;

            return this.View(query);
        }

        [HttpGet]
        public IActionResult Schedule()
        {
            var model = new ScheduleViewModel();

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Plans()
        {
            var model = await this.danceService.GetAllPlans();

            return this.View(model);
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new PlanViewModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Create(PlanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await danceService.CreatePlan(model);

                return RedirectToAction("Plans", "Class");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);

            }
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
            var model = new AddClassViewModel()
            {
                LevelsCategory = await this.levelCategoryService.AllCategories(),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Add(AddClassViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.LevelsCategory = await this.levelCategoryService.AllCategories();

                return this.View(model);
            }
            try
            {
                await this.danceService.CreateClassAsync(model);

                return this.RedirectToAction("Classes", "Class");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");

                return this.View(model);

            }

        }

        //When user is not administrator let's not See the button 

        public async Task<IActionResult> StartClass(int classId)
        {
            try
            {
                var userId = this.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;


                if (this.User.IsInRole("Administrator"))
                {
                    throw new ArgumentException(
                        string.Format(ExceptionMessages.AdminHaveNotClass, userId)
                        );
                }

                await danceService.AddStudentToClass(userId, classId);

                return this.RedirectToAction(nameof(this.Plans));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> LeaveClass(int classId)
        {


            var userId = this.User.Claims
                        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            await danceService.LeaveClass(classId, userId);

            return this.RedirectToAction(nameof(this.Classes));


        }


        public async Task<IActionResult> Train()
        {


            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var model = await this.danceService.GetMyClassAsync(userId);
            return View("Training", model);


        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditDance(int id)
        {
            if ((await danceService.Exists(id)) == false)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.ClassDanceNotFound, id));
                return RedirectToAction(nameof(this.Classes));
            }

            var danceClass = await this.danceService.DanceDetailsById(id);
            var levelCategoryId = await this.danceService.GetDanceLevelId(id);

            var model = new EditDanceViewModel()
            {
                Id = id,
                Name = danceClass.Name,
                Instructor = danceClass.Instructor,
                Description = danceClass.Description,
                LevelCategoryId = levelCategoryId,
                ImageUrl = danceClass.ImageUrl,
                LevelsCategory = await this.levelCategoryService.AllCategories(),
            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditDance(int id, EditDanceViewModel model)
        {
            if ((await danceService.Exists(model.Id)) == false)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.ClassDanceNotFound, model.Id));
                model.LevelsCategory = await this.levelCategoryService.AllCategories();
                return View(model);
            }
            if (ModelState.IsValid == false)
            {
                model.LevelsCategory = await this.levelCategoryService.AllCategories();
                return View(model);
            }

            await danceService.Edit(model.Id, model);

            return this.RedirectToAction(nameof(this.Classes));
        }

    }
}
