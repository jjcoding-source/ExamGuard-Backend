namespace ExamGuard.API.DTOs
{
    public class CreateExamDto
    {
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}