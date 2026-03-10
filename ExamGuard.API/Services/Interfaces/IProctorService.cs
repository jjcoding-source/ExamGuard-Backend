using ExamGuard.API.DTOs;

namespace ExamGuard.API.Services.Interfaces
{
    public interface IProctorService
    {
        Task<bool> LogEventAsync(LogEventDto dto);
        Task<IEnumerable<ProctorEventResponseDto>> GetEventsAsync(int sessionId);
        Task<bool> TerminateSessionAsync(int sessionId);
    }
}