using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Models;
using JustDanceAcademy.Web.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Services.Data.Constants
{
    public interface IDanceClassService
    {
        Task<IEnumerable<ClassesViewModel>> GetAllAsync();

       

        Task<int> CreateClassAsync(AddClassViewModel model);
    }
}
