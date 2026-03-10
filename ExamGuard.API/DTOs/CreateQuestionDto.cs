namespace ExamGuard.API.DTOs
{
    public class CreateQuestionDto
    {
        public string Text { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new();
        public int CorrectIndex { get; set; }
    }
}