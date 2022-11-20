using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.ViewModels.Models
{
    public class InstructorsViewModel
    {
        public int Id
        {
            get; set;
        }

        public string FullName
        {
            get; set;
        }

        public string ImageUrl { get; set; } = null!;



        public string Class
        {
            get; set;
        }

        public string Intro
        {
            get; set;
        }
    }
}
