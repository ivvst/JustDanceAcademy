using JustDanceAcademy.Data.Common.Models;
using JustDanceAcademy.Data.Models.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Data.Models
{
    public class MemberShip : BaseDeletableModel<int>
    {

        public int Id
        {
            get; set;
        }

        // MONTH
        public string Title
        {
            get; set;
        }

        public Age Age
        {
            get; set;
        }

        [Column(TypeName = "decimal(18,0)")]
        public decimal Price
        {
            get; set;
        }

        public string DetailOne
        {
            get; set;
        }

        public string DetailTwo
        {
            get; set;
        }

        public string DetailThree
        {
            get; set;
        }



    }
}
