namespace ExamGuard.API.DTOs
{
    public class ProctorEventResponseDto
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}