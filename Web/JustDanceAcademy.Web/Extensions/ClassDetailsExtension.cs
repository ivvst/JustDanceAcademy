namespace JustDanceAcademy.Services.Data.Extensions
{
	using System;
	using System.Linq;
	using System.Text;
	using System.Text.RegularExpressions;

	using JustDanceAcademy.Web.ViewModels.Contracts;

	public static class ClassDetailsExtension
	{
		public static string GetInformation(this IClassModel dance)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(dance.Name.Replace(" ", "-"));
			sb.Append("-");
			sb.Append(GetDescript(dance.Description));

			return sb.ToString();

		}

		private static string GetDescript(string description)
		{
			string result = string
				.Join("-", description.Split(" ", StringSplitOptions.RemoveEmptyEntries).Take(4));

			return Regex.Replace(description, @"[^a-zA-Z0-9\-]", string.Empty);
		}
	}
}
