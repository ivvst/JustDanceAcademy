using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDanceAcademy.Services.Data.Common
{
    public class ExceptionMessages

    {
        public const string ReviewNotAllowed = "Review for danceClass is not allowed.";

        public const string StudentNotFound = "Student is not found.";

        public const string ClassDanceNotFound = "Dance-Class  is not found.";

        public const string InstructorNotFound = "Instructor is not found.";

        public const string InvalidDanceCategoryType = "Dance category is invalid.";

        public const string InstructorAlreadyExists = "Instructor already exists";

        public const string AdminHaveNotClass = "Admin can not have StudentLisence ";


        public const string ClassAlreadyIsStarted = " The Class is not added because you already start a class.";
    }
}
