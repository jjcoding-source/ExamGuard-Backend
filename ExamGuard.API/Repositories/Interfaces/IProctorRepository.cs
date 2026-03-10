using ExamGuard.API.Models;

namespace ExamGuard.API.Repositories.Interfaces
{
    public interface IProctorRepository
    {
        Task<ProctorEvent> CreateAsync(ProctorEvent proctorEvent);
        Task<IEnumerable<ProctorEvent>> GetBySessionAsync(int sessionId);
    }
}