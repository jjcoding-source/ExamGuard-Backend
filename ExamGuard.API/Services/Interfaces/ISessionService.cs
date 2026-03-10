using ExamGuard.API.DTOs;

namespace ExamGuard.API.Services.Interfaces
{
    public interface ISessionService
    {
        Task<SessionResponseDto?> StartAsync(int examId, int studentId);
        Task<SessionResponseDto?> SubmitAsync(SubmitSessionDto dto, int studentId);
        Task<SessionResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<SessionResponseDto>> GetByExamAsync(int examId);
        Task<IEnumerable<SessionResponseDto>> GetMySessionsAsync(int studentId);
    }
}