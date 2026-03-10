using ExamGuard.API.Data;
using ExamGuard.API.Models;
using ExamGuard.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamGuard.API.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _context;

        public SessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ExamSession?> GetByIdAsync(int id)
            => await _context.ExamSessions.FindAsync(id);

        public async Task<ExamSession?> GetWithDetailsAsync(int id)
            => await _context.ExamSessions
                .Include(s => s.Exam)
                .Include(s => s.Student)
                .Include(s => s.Answers)
                    .ThenInclude(a => a.Question)
                .Include(s => s.ProctorEvents)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<IEnumerable<ExamSession>> GetByExamAsync(int examId)
            => await _context.ExamSessions
                .Include(s => s.Student)
                .Include(s => s.ProctorEvents)
                .Where(s => s.ExamId == examId)
                .ToListAsync();

        public async Task<IEnumerable<ExamSession>> GetByStudentAsync(int studentId)
            => await _context.ExamSessions
                .Include(s => s.Exam)
                .Where(s => s.StudentId == studentId)
                .ToListAsync();

        public async Task<ExamSession?> GetActiveSessionAsync(int examId, int studentId)
            => await _context.ExamSessions
                .FirstOrDefaultAsync(s =>
                    s.ExamId == examId &&
                    s.StudentId == studentId &&
                    s.Status == "Active");

        public async Task<ExamSession> CreateAsync(ExamSession session)
        {
            _context.ExamSessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<ExamSession> UpdateAsync(ExamSession session)
        {
            _context.ExamSessions.Update(session);
            await _context.SaveChangesAsync();
            return session;
        }
    }
}