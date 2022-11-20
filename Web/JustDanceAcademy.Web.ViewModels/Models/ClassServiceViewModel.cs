using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.ViewModels.Models
{
    public class ClassServiceViewModel
    {
        public int Id
        {
            get; init;
        }

        public string Name
        {
            get; init;
        } 
            = null!;

        public string Instructor
        {
            get; init;
        } = null!;

        public string ImageUrl
        {
            get; init;
        } = null!;
    }
}
