namespace JustDanceAcademy.Services.Data.Constants
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Models;

	public interface ILevelCategoryService
	{
		Task<IEnumerable<LevelCategory>> AllCategories();

		Task<string> DoesNameOfDanceCategoryExist(string name);
	}


}
