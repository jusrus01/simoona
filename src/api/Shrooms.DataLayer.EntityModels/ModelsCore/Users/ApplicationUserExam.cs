namespace Shrooms.DataLayer.EntityModels.ModelsCore.Users
{
    public class ApplicationUserExam
    {
        public int ExamId { get; set; }

        public Exam Exam { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
