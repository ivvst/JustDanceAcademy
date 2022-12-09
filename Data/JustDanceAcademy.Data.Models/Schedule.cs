using JustDanceAcademy.Data.Common.Models;
using JustDanceAcademy.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Data.Models
{
    public class Schedule:BaseDeletableModel<int>
    {
        public int Id
        {
            get; set;
        }

         // CLASS NAME
        public int ClassId
        {
            get; set;
        }

        public Class Class
        {
            get; set;
        }

     
        public DateTime StartTime
        {
            get;set;
        }

        public DateTime EndTime
        {
            get; set;
        }

        public Day Day
        {
            get; set;
        }

        public Age Age
        {

            get; set;
        }

       
    }
}
