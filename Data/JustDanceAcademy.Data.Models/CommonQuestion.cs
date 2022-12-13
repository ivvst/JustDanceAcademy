namespace JustDanceAcademy.Data.Models
{
	using JustDanceAcademy.Data.Common.Models;

	public class CommonQuestion : BaseDeletableModel<int>
	{
        public int Id
		{
			get; set;
		}

        public string Question
		{
			get; set;
		}

        public string Answear
		{
			get; set;
		}
	}
}
