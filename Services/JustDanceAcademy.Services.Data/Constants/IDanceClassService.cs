using DanceAcademy.Models;
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

        Task<int> CreatePlan(PlanViewModel model);

        Task<IEnumerable<PlanViewModel>> GetAllPlans();

        Task<ClassQueryModel> All(
            string? category = null,
            string? searchTerm = null,
            int currentPage = 1,
            int classPerPage = 1
            );

        Task<IEnumerable<string>> AllCategoriesNames();
    }
}







