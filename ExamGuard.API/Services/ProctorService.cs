using ExamGuard.API.DTOs;
using ExamGuard.API.Enums;
using ExamGuard.API.Models;
using ExamGuard.API.Repositories.Interfaces;
using ExamGuard.API.Services.Interfaces;

namespace ExamGuard.API.Services
{
    public class ProctorService : IProctorService
    {
        private readonly IProctorRepository _proctorRepo;
        private readonly ISessionRepository _sessionRepo;

        private static readonly Dictionary<string, int> Deductions = new()
        {
            { ViolationType.NoFace,         10 },
            { ViolationType.MultipleFaces,  20 },
            { ViolationType.LookingAway,     5 },
            { ViolationType.TabSwitch,       8 },
            { ViolationType.FullscreenExit,  8 },
            { ViolationType.CopyPaste,       5 },
            { ViolationType.RightClick,      2 },
            { ViolationType.CameraDenied,   15 },
        };

        public ProctorService(
            IProctorRepository proctorRepo,
            ISessionRepository sessionRepo)
        {
            _proctorRepo = proctorRepo;
            _sessionRepo = sessionRepo;
        }

        public async Task<bool> LogEventAsync(LogEventDto dto)
        {
            var session = await _sessionRepo.GetByIdAsync(dto.SessionId);
            if (session == null || session.Status != SessionStatus.Active)
                return false;

            // Deduct from trust score
            if (Deductions.TryGetValue(dto.Type, out int deduction))
            {
                session.TrustScore = Math.Max(0, session.TrustScore - deduction);
            }

            session.ViolationCount++;
            await _sessionRepo.UpdateAsync(session);

            // Log the event
            var proctorEvent = new ProctorEvent
            {
                SessionId = dto.SessionId,
                Type = dto.Type,
                Severity = dto.Severity,
                Message = dto.Message,
                Timestamp = dto.Timestamp,
            };

            await _proctorRepo.CreateAsync(proctorEvent);
            return true;
        }

        public async Task<IEnumerable<ProctorEventResponseDto>> GetEventsAsync(int sessionId)
        {
            var events = await _proctorRepo.GetBySessionAsync(sessionId);
            return events.Select(e => new ProctorEventResponseDto
            {
                Id = e.Id,
                SessionId = e.SessionId,
                Type = e.Type,
                Severity = e.Severity,
                Message = e.Message,
                Timestamp = e.Timestamp,
            });
        }

        public async Task<bool> TerminateSessionAsync(int sessionId)
        {
            var session = await _sessionRepo.GetByIdAsync(sessionId);
            if (session == null) return false;

            session.Status = SessionStatus.Terminated;
            session.EndTime = DateTime.UtcNow;

            await _sessionRepo.UpdateAsync(session);
            return true;
        }
    }
}