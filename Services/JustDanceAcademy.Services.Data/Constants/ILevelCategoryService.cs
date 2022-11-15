using JustDanceAcademy.Web.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Services.Data.Constants
{
    public  interface ILevelCategoryService
    {
        Task<IEnumerable<LevelDanceViewModel>> AllCategories();
    }
}
