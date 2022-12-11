namespace JustDanceAcademy.Services.Data.Constants
{
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Web.ViewModels.Models;
	using System.Collections;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IFaQService
	{

		public Task<int> CreateQuestWithAnswer(QuestViewModel model);

		public Task<bool> IsQuestionAdded(string question);

		public Task<IEnumerable<CommonQuestion>> GetAllAsync();

		// Edit Quest //DELETE QUEST SHOW ALL QUEST
	}
}
