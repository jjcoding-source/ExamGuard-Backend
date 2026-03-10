using ExamGuard.API.DTOs;
using ExamGuard.API.Enums;
using ExamGuard.API.Models;
using ExamGuard.API.Repositories.Interfaces;
using ExamGuard.API.Services.Interfaces;

namespace ExamGuard.API.Services
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepo;

        public ExamService(IExamRepository examRepo)
        {
            _examRepo = examRepo;
        }

        public async Task<IEnumerable<ExamResponseDto>> GetAllAsync(int userId, string role)
        {
            var exams = role == UserRole.Instructor
                ? await _examRepo.GetByInstructorAsync(userId)
                : await _examRepo.GetAllAsync();

            return exams.Select(MapToDto);
        }

        public async Task<ExamResponseDto?> GetByIdAsync(int id)
        {
            var exam = await _examRepo.GetByIdAsync(id);
            return exam == null ? null : MapToDto(exam);
        }

        public async Task<ExamResponseDto> CreateAsync(CreateExamDto dto, int instructorId)
        {
            var exam = new Exam
            {
                Title = dto.Title,
                Code = dto.Code,
                DurationMinutes = dto.DurationMinutes,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = ExamStatus.Upcoming,
                InstructorId = instructorId,
                CreatedAt = DateTime.UtcNow,
            };

            await _examRepo.CreateAsync(exam);
            var created = await _examRepo.GetByIdAsync(exam.Id);
            return MapToDto(created!);
        }

        public async Task<ExamResponseDto?> UpdateAsync(int id, CreateExamDto dto, int instructorId)
        {
            var exam = await _examRepo.GetByIdAsync(id);
            if (exam == null || exam.InstructorId != instructorId) return null;

            exam.Title = dto.Title;
            exam.Code = dto.Code;
            exam.DurationMinutes = dto.DurationMinutes;
            exam.StartTime = dto.StartTime;
            exam.EndTime = dto.EndTime;

            await _examRepo.UpdateAsync(exam);
            return MapToDto(exam);
        }

        public async Task<bool> DeleteAsync(int id, int instructorId)
        {
            var exam = await _examRepo.GetByIdAsync(id);
            if (exam == null || exam.InstructorId != instructorId) return false;

            await _examRepo.DeleteAsync(id);
            return true;
        }

        private static ExamResponseDto MapToDto(Exam exam) => new()
        {
            Id = exam.Id,
            Title = exam.Title,
            Code = exam.Code,
            DurationMinutes = exam.DurationMinutes,
            StartTime = exam.StartTime,
            EndTime = exam.EndTime,
            Status = exam.Status,
            InstructorName = exam.Instructor?.Name ?? string.Empty,
            StudentCount = exam.Sessions?.Count ?? 0,
        };
    }
}