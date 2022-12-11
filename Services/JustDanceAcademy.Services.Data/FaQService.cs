namespace JustDanceAcademy.Services.Data
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using JustDanceAcademy.Data.Common.Repositories;
	using JustDanceAcademy.Data.Models;
	using JustDanceAcademy.Services.Data.Constants;
	using JustDanceAcademy.Web.ViewModels.Models;
	using Microsoft.EntityFrameworkCore;

	public class FaQService : IFaQService
	{
		private readonly IRepository<CommonQuestion> questionRepository;

		public FaQService(IRepository<CommonQuestion> questionRepository)
		{
			this.questionRepository = questionRepository;
		}

		public async Task<int> CreateQuestWithAnswer(QuestViewModel model)
		{
			var entity = new CommonQuestion()
			{
				Id = model.Id,
				Question = model.Question,
				Answear = model.Answer,
			};

			// bool is question is Added in controller;
			await this.questionRepository.AddAsync(entity);
			await this.questionRepository.SaveChangesAsync();
			return entity.Id;
		}

		public async Task<IEnumerable<CommonQuestion>> GetAllAsync()
		{
			var result = await this.questionRepository.AllAsNoTracking().ToListAsync();
			return result;
		}

		public async Task<bool> IsQuestionAdded(string question)
		{
			var result = await this.questionRepository.All().AnyAsync(x => x.Question == question);
			return result;
		}
	}
}
