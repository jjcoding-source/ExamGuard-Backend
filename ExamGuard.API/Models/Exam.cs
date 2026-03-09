namespace ExamGuard.API.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = "Upcoming"; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int InstructorId { get; set; }

        public User Instructor { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<ExamSession> Sessions { get; set; } = new List<ExamSession>();
    }
}