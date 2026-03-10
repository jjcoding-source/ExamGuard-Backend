using ExamGuard.API.DTOs;
using ExamGuard.API.Enums;
using ExamGuard.API.Models;
using ExamGuard.API.Repositories.Interfaces;
using ExamGuard.API.Services.Interfaces;

namespace ExamGuard.API.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepo;
        private readonly IExamRepository _examRepo;

        public SessionService(
            ISessionRepository sessionRepo,
            IExamRepository examRepo)
        {
            _sessionRepo = sessionRepo;
            _examRepo = examRepo;
        }

        public async Task<SessionResponseDto?> StartAsync(int examId, int studentId)
        {
            var exam = await _examRepo.GetByIdAsync(examId);
            if (exam == null) return null;

            // Check if already has active session
            var existing = await _sessionRepo.GetActiveSessionAsync(examId, studentId);
            if (existing != null) return MapToDto(existing);

            var session = new ExamSession
            {
                ExamId = examId,
                StudentId = studentId,
                StartTime = DateTime.UtcNow,
                Status = SessionStatus.Active,
                TrustScore = 100,
            };

            await _sessionRepo.CreateAsync(session);
            var created = await _sessionRepo.GetWithDetailsAsync(session.Id);
            return MapToDto(created!);
        }

        public async Task<SessionResponseDto?> SubmitAsync(SubmitSessionDto dto, int studentId)
        {
            var session = await _sessionRepo.GetWithDetailsAsync(dto.SessionId);
            if (session == null || session.StudentId != studentId) return null;
            if (session.Status != SessionStatus.Active) return null;

            int correct = 0;
            foreach (var answerDto in dto.Answers)
            {
                var question = session.Exam.Questions
                    .FirstOrDefault(q => q.Id == answerDto.QuestionId);

                if (question == null) continue;

                var isCorrect = question.CorrectIndex == answerDto.SelectedIndex;
                if (isCorrect) correct++;

                session.Answers.Add(new Answer
                {
                    SessionId = session.Id,
                    QuestionId = answerDto.QuestionId,
                    SelectedIndex = answerDto.SelectedIndex,
                    IsCorrect = isCorrect,
                });
            }

            session.Status = SessionStatus.Submitted;
            session.EndTime = DateTime.UtcNow;

            await _sessionRepo.UpdateAsync(session);
            return MapToDto(session);
        }

        public async Task<SessionResponseDto?> GetByIdAsync(int id)
        {
            var session = await _sessionRepo.GetWithDetailsAsync(id);
            return session == null ? null : MapToDto(session);
        }

        public async Task<IEnumerable<SessionResponseDto>> GetByExamAsync(int examId)
        {
            var sessions = await _sessionRepo.GetByExamAsync(examId);
            return sessions.Select(MapToDto);
        }

        public async Task<IEnumerable<SessionResponseDto>> GetMySessionsAsync(int studentId)
        {
            var sessions = await _sessionRepo.GetByStudentAsync(studentId);
            return sessions.Select(MapToDto);
        }

        private static SessionResponseDto MapToDto(ExamSession s) => new()
        {
            Id = s.Id,
            ExamId = s.ExamId,
            ExamTitle = s.Exam?.Title ?? string.Empty,
            ExamCode = s.Exam?.Code ?? string.Empty,
            StudentName = s.Student?.Name ?? string.Empty,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            Status = s.Status,
            TrustScore = s.TrustScore,
            ViolationCount = s.ViolationCount,
        };
    }
}