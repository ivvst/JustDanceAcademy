using DanceAcademy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanceAcademy.Controllers
{

    public class UserController : Controller
    {
 
        [HttpGet]
        public IActionResult Plan()
        {
            var model = new PlanViewModel();
            return this.View(model);
        }

        
        [HttpGet]

        public IActionResult Instructors()
        {
            var model = new InstructorViewModel();
            return this.View(model);
        }

        [HttpGet]
        public IActionResult FeedBack()
        {
            var model = new FeedBackViewModel();
            return View(model);
        }


    }
}
