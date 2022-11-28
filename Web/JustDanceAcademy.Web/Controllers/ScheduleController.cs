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

      
        
    }
}
