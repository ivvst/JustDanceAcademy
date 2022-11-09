using DanceAcademy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanceAcademy.Controllers
{

    public class UserController : Controller
    {
        //private readonly UserManager<ApplicationUser> userManager;
        //private readonly SignInManager<ApplicationUser> signInManager;
        //public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        //{
        //    this.userManager = userManager;
        //    this.signInManager = signInManager;
        //}



        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Register()
        //{

        //    var model = new RegisterViewModel();

        //    return this.View(model);
        //}
        [HttpGet]
        public IActionResult Register()
        {
            var model = new LoginViewModel();
            return this.View(model);
        }
        [HttpGet]
        public IActionResult About()
        {
            var model = new AboutViewModel();
            return this.View(model);
        }
       
        [HttpGet]
        public IActionResult Classes()
        {
            var model = new ClassesViewModel();
            return this.View(model);
        }
        [HttpGet]
        public IActionResult Plan()
        {
            var model = new PlanViewModel ();
            return this.View(model);
        }
        [HttpGet]
        public IActionResult Instructors()
        {
            var model = new GalleryViewModel();
            return this.View(model);
        }
        [HttpGet]
        public IActionResult Gallery()
        {
            var model = new GalleryViewModel();
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
