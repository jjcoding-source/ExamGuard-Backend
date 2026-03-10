namespace ExamGuard.API.DTOs
{
    public class QuestionResponseDto
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new();
        public int CorrectIndex { get; set; }
        public int OrderIndex { get; set; }
    }
}
