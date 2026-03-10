using ExamGuard.API.Data;
using ExamGuard.API.Models;
using ExamGuard.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamGuard.API.Repositories
{
    public class ProctorRepository : IProctorRepository
    {
        private readonly AppDbContext _context;

        public ProctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProctorEvent> CreateAsync(ProctorEvent proctorEvent)
        {
            _context.ProctorEvents.Add(proctorEvent);
            await _context.SaveChangesAsync();
            return proctorEvent;
        }

        public async Task<IEnumerable<ProctorEvent>> GetBySessionAsync(int sessionId)
            => await _context.ProctorEvents
                .Where(p => p.SessionId == sessionId)
                .OrderBy(p => p.Timestamp)
                .ToListAsync();
    }
}
