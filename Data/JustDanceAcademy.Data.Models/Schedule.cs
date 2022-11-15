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
    public class Schedule
    {
        public int Id
        {
            get; set;
        }
        public string LevelCategory
        {
            get; set;
        }

       

        public DateTime StartTime
        {
            get;set;
        }
        public DateTime EndTime
        {
            get;set;
        }

        public Day Day
        {
            get;set;
        }

        [MaxLength(200)]
        public string Notes
        {
            get; set;
        }
    }
}
