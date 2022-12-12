using System.Data;

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

		public const string UserAlreadyPaid = "You already pay for your Train-Class";

		public const string StartClass = "Welcome to your Class";

		public const string LoginError = "Your Login Failed";

		public const string RegisterError = "Your Register Failed.";

		public const string UserNameTaken = "Username is already taken.";

		public const string EmailTaken = "Email is already taken.";

		public const string MustPay = "You have to pay for the class.";

		public const string LeaveClass = "You leave the class.. Now you can start new one!!";

		public const string ClassAlreadyIsStarted = " The Class is not added because you already start a class.";

		public const string QuestionAlreadyExists = "Question already exists";

		public const string TrainerHasClass = "Trainer still have Class.";


	}
}
