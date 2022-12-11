// ReSharper disable VirtualMemberCallInConstructor
namespace JustDanceAcademy.Data.Models
{
    using System;
    using System.Collections.Generic;

    using JustDanceAcademy.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Reviews = new HashSet<Review>();


        }



        // Audit info
        public DateTime CreatedOn
        {
            get; set;
        }

        public DateTime? ModifiedOn
        {
            get; set;
        }

        // Deletable entity
        public bool IsDeleted
        {
            get; set;
        }

        public DateTime? DeletedOn
        {
            get; set;
        }

        public int? ClassId
        {
            get; set;
        }

        public virtual Class Class
        {
            get; set;
        }

		 public int? PlanId
		{
			get; set;
		}

		public virtual MemberShip Plan
		{
			get; set;
		}




		public virtual ICollection<IdentityUserRole<string>> Roles
        {
            get; set;
        }

        public virtual ICollection<IdentityUserClaim<string>> Claims
        {
            get; set;
        }

        public virtual ICollection<IdentityUserLogin<string>> Logins
        {
            get; set;
        }
        public virtual ICollection<Review> Reviews
        {
            get; set;
        }


    }

}

