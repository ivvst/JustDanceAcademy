using DanceAcademy.Models;
using JustDanceAcademy.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DanceAcademy.Controllers
{
    public class HomeController : BaseController 
    {
        [HttpGet]
        public IActionResult Index()
        {

            var model = new HomeViewModel();
            return this.View(model);

        }

       
    }
}