namespace ExamGuard.API.DTOs
{
    public class SessionResponseDto
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string ExamTitle { get; set; } = string.Empty;
        public string ExamCode { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public int TrustScore { get; set; }
        public int ViolationCount { get; set; }
        public int? ExamScore { get; set; }
    }
}