namespace ExamGuard.API.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int SelectedIndex { get; set; }
        public bool IsCorrect { get; set; }

        public int SessionId { get; set; }
        public int QuestionId { get; set; }

        public ExamSession Session { get; set; } = null!;
        public Question Question { get; set; } = null!;
    }
}