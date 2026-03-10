using ExamGuard.API.Models;

namespace ExamGuard.API.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        Task<ExamSession?> GetByIdAsync(int id);
        Task<ExamSession?> GetWithDetailsAsync(int id);
        Task<IEnumerable<ExamSession>> GetByExamAsync(int examId);
        Task<IEnumerable<ExamSession>> GetByStudentAsync(int studentId);
        Task<ExamSession?> GetActiveSessionAsync(int examId, int studentId);
        Task<ExamSession> CreateAsync(ExamSession session);
        Task<ExamSession> UpdateAsync(ExamSession session);
    }
}