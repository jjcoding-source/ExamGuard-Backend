namespace ExamGuard.API.Models
{
    public class ExamSession
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }
        public string Status { get; set; } = "Active"; 
        public int TrustScore { get; set; } = 100;
        public int ViolationCount { get; set; } = 0;

        public int ExamId { get; set; }
        public int StudentId { get; set; }

        public Exam Exam { get; set; } = null!;
        public User Student { get; set; } = null!;
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public ICollection<ProctorEvent> ProctorEvents { get; set; } = new List<ProctorEvent>();
    }
}