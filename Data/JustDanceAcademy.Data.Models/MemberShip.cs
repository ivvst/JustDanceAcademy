using JustDanceAcademy.Data.Common.Models;
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

        public string Title
        {
            get; set;
        }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }


    }
}
