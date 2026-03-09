namespace ExamGuard.API.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public string Options { get; set; } = string.Empty;
        public int CorrectIndex { get; set; }
        public int OrderIndex { get; set; }

        public int ExamId { get; set; }

        public Exam Exam { get; set; } = null!;
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}