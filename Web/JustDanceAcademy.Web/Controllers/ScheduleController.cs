using JustDanceAcademy.Services.Data.Constants;
using JustDanceAcademy.Web.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.Controllers
{
    public class ScheduleController : BaseController
    {
        private readonly IScheduleService scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }


        [HttpGet]
        public async  Task <IActionResult> SeeAll()
        {
            var model = await this.scheduleService.AllSchedules();
            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddScheduleViewModel()
            {
                AllClasses = await this.scheduleService.GetClasses(),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddScheduleViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.AllClasses = await this.scheduleService.GetClasses();
                return this.View(model);
            }

            try
            {
                await this.scheduleService.CreateSchedule(model);


                return this.RedirectToAction("SeeAll", "Schedule");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError("", "Something went wrong");

                return this.View(model);
            }
        }
    }
}
