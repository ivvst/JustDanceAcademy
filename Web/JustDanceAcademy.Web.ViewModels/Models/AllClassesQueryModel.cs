using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Web.ViewModels.Models
{
    public class AllClassesQueryModel
    {

        public const int ClassesPerPage = 3;

        public string? Category
        {
            get; set;
        }

        public string SearchTerm
        {
            get; set;
        }

        public int CurrentPage
        {
            get; set;
        } = 1;

        public int TotalClassesCount
        {
            get; set;
        } 

        public IEnumerable<string> LevelsCategory { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<ClassServiceViewModel> Classes { get; set; } = Enumerable.Empty<ClassServiceViewModel>();
    }
}
