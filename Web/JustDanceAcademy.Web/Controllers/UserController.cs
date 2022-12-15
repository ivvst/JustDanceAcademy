namespace JustDanceAcademy.Controllers
{
    using JustDanceAcademy.Models;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        public IActionResult About()
        {
            var model = new HomeViewModel();
            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Terms()
        {
            return this.View();
        }
    }
}
