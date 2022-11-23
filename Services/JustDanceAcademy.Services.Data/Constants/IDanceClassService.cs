﻿using DanceAcademy.Models;
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

        Task<ClassesViewModel> DanceDetailsById(int id);
        Task<int> CreateClassAsync(AddClassViewModel model);

        Task<int> GetDanceLevelId(int classId);

        Task<bool> Exists(int id);

        Task Edit(int classId, EditDanceViewModel model);

        Task<int> CreatePlan(PlanViewModel model);

        Task AddStudentToClass(string userId, int classId);

        Task LeaveClass(int classId, string userId);

        Task<IEnumerable<MyClassViewModel>> GetMyClassAsync(string userId);





        Task<IEnumerable<PlanViewModel>> GetAllPlans();

        Task<ClassQueryModel> All(
            string? category = null,
            string? searchTerm = null,
            int currentPage = 1,
            int classPerPage = 1
            );

        Task<IEnumerable<string>> AllCategoriesNames();


        //Task<IEnumerable<ClassesAndStudentsViewModel>> GetInfo();


    }
}







