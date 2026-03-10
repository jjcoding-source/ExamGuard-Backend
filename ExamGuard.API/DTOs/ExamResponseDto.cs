namespace ExamGuard.API.DTOs
{
    public class ExamResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public List<QuestionResponseDto> Questions { get; set; } = new();
    }
}