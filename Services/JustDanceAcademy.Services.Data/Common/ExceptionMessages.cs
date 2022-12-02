using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Services.Data.Common
{
    public  class ExceptionMessages

    {
        public const string ReviewNotAllowed = "Review for movie with id {0} is not allowed.";

        public const string StudentNotFound = "Student with id {0} is not found.";

        public const string ClassDanceNotFound = "Dance-Class  is not found.";

        public const string InstructorNotFound = "Instructor with id {0} is not found.";

        public const string InvalidDanceCategoryType = "Dance category type {0} is invalid.";

        public const string InstructorAlreadyExists = "Instructor with name {0} already exists";

        public const string AdminHaveNotClass = "Admin with {0} can not have StudentLisence ";


        public const string ClassAlreadyIsStarted = "Class with id {0} is not added because you already start a class.";
    }
}
