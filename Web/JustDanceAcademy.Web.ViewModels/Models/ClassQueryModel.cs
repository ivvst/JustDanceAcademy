using System.Collections.Generic;

namespace JustDanceAcademy.Web.ViewModels.Models
{
    public class ClassQueryModel
    {
        public int TotalClassesCount
        {
            get; set;
        }

        public IEnumerable<ClassServiceViewModel> Classes
        {
            get; set;
        } = new List<ClassServiceViewModel>();
    }
}
