using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustDanceAcademy.Data.Models;
using JustDanceAcademy.Data.Models.Enum;

namespace JustDanceAcademy.Web.ViewModels.Models
{
    public class PlanViewModel
    {

        [Required]
        [StringLength(25, ErrorMessage = "{0} must be choose carefully with {1} letters", MinimumLength = 5)]
        public string Title
        {
            get; set;
        }

        [Required]
        [Range(typeof(decimal), "0.00", "500.00", ConvertValueInInvariantCulture = true)]
        public decimal Price
        {
            get; set;
        }

        [Required]
        public Age Age
        {
            get; set;
        }

        public string AgeRequirement
        {
            get; set;
        }
    }
}
