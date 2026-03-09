namespace ExamGuard.API.DTOs
{
    public class SubmitSessionDto
    {
        public int SessionId { get; set; }
        public List<AnswerDto> Answers { get; set; } = new();
    }

    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public int SelectedIndex { get; set; }
    }
}