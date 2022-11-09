using JustDanceAcademy.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Data.Models
{
    public class Class : BaseDeletableModel<int>
    {
        [Required]
        public string Name
        {
            get; set;
        }

        public string ImageUrl
        {
            get; set;
        }

        [Required]
        public string Description
        {
            get; set;
        }

        public int InstrustorId
        {
            get; set;
        }

        [ForeignKey(nameof(InstrustorId))]
        public Instrustor Instrustor
        {
            get; set;
        }

        public virtual ICollection<Plan> Plans
        {
            get;

            set;
        }
    }
}
