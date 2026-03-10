using ExamGuard.API.Models;

namespace ExamGuard.API.Repositories.Interfaces
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetAllAsync();
        Task<IEnumerable<Exam>> GetByInstructorAsync(int instructorId);
        Task<Exam?> GetByIdAsync(int id);
        Task<Exam?> GetWithQuestionsAsync(int id);
        Task<Exam> CreateAsync(Exam exam);
        Task<Exam> UpdateAsync(Exam exam);
        Task DeleteAsync(int id);
        Task AddQuestionAsync(Question question);
        Task<bool> DeleteQuestionAsync(int questionId);
        Task<int> GetQuestionCountAsync(int examId);
    }
}