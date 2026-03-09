

namespace ExamGuard.API.Models
{
    public class ProctorEvent
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;      
        public string Severity { get; set; } = string.Empty;  
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public int SessionId { get; set; }

        public ExamSession Session { get; set; } = null!;
    }
}
