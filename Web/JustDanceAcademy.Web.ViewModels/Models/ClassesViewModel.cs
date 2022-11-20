namespace JustDanceAcademy.Models
{
     public class ClassesViewModel
    {
        public int Id
        {
            get; set;
        }

        public string Name { get; set; } = null!;

        public string Instructor { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Description
        {
            get; set;
        }

        public string Category
        {
            get;set;
        }

    }
}
