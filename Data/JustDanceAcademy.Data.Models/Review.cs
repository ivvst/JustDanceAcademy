namespace JustDanceAcademy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using JustDanceAcademy.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        public int Id
        {
            get; set;
        }

        [Required]
        [StringLength(500)]
        public string Content
        {
            get; set;
        }

        public int ClassId
        {
            get; set;
        }

        public virtual Class Class
        {
            get; set;
        }

        public string UserId
        {
            get; set;
        }

        public virtual ApplicationUser User
        {
            get; set;
        }
    }
}
