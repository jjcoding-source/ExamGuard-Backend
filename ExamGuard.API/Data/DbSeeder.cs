using ExamGuard.API.Enums;
using ExamGuard.API.Models;

namespace ExamGuard.API.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)

            if (context.Users.Any()) return;

            var admin = new User
            {
                Name = "System Admin",
                Email = "admin@examguard.io",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = UserRole.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            var instructor = new User
            {
                Name = "Alex Johnson",
                Email = "alex@university.edu",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Instructor@123"),
                Role = UserRole.Instructor,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            var student1 = new User
            {
                Name = "Riya Sharma",
                Email = "riya@student.edu",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                Role = UserRole.Student,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            var student2 = new User
            {
                Name = "Kevin Park",
                Email = "kpark@student.edu",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                Role = UserRole.Student,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            context.Users.AddRange(admin, instructor, student1, student2);
            await context.SaveChangesAsync();

            var exam1 = new Exam
            {
                Title = "Data Structures Midterm",
                Code = "CS301",
                DurationMinutes = 120,
                StartTime = DateTime.UtcNow.AddHours(-1),
                EndTime = DateTime.UtcNow.AddHours(1),
                Status = ExamStatus.Live,
                InstructorId = instructor.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var exam2 = new Exam
            {
                Title = "Algorithm Analysis",
                Code = "CS402",
                DurationMinutes = 90,
                StartTime = DateTime.UtcNow.AddHours(2),
                EndTime = DateTime.UtcNow.AddHours(4),
                Status = ExamStatus.Upcoming,
                InstructorId = instructor.Id,
                CreatedAt = DateTime.UtcNow,
            };

            context.Exams.AddRange(exam1, exam2);
            await context.SaveChangesAsync();

            var questions = new List<Question>
            {
                new Question
                {
                    ExamId       = exam1.Id,
                    Text         = "Which data structure uses LIFO ordering?",
                    Options      = "Queue|Stack|Linked List|Heap",
                    CorrectIndex = 1,
                    OrderIndex   = 1,
                },
                new Question
                {
                    ExamId       = exam1.Id,
                    Text         = "What is the time complexity of binary search?",
                    Options      = "O(n)|O(n²)|O(log n)|O(1)",
                    CorrectIndex = 2,
                    OrderIndex   = 2,
                },
                new Question
                {
                    ExamId       = exam1.Id,
                    Text         = "What is the worst-case time complexity of QuickSort?",
                    Options      = "O(n log n)|O(n)|O(n²)|O(log n)",
                    CorrectIndex = 2,
                    OrderIndex   = 3,
                },
            };

            context.Questions.AddRange(questions);
            await context.SaveChangesAsync();
        }
    }
}
