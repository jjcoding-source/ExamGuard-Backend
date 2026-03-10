using ExamGuard.API.DTOs;

namespace ExamGuard.API.Services.Interfaces
{
    public interface IExamService
    {
        Task<IEnumerable<ExamResponseDto>> GetAllAsync(int userId, string role);
        Task<ExamResponseDto?> GetByIdAsync(int id);
        Task<ExamResponseDto> CreateAsync(CreateExamDto dto, int instructorId);
        Task<ExamResponseDto?> UpdateAsync(int id, CreateExamDto dto, int instructorId);
        Task<bool> DeleteAsync(int id, int instructorId);
    }
}