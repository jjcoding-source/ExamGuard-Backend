using ExamGuard.API.Data;
using ExamGuard.API.Models;
using ExamGuard.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamGuard.API.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly AppDbContext _context;

        public ExamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exam>> GetAllAsync()
            => await _context.Exams
                .Include(e => e.Instructor)
                .Include(e => e.Sessions)
                .ToListAsync();

        public async Task<IEnumerable<Exam>> GetByInstructorAsync(int instructorId)
            => await _context.Exams
                .Include(e => e.Instructor)
                .Include(e => e.Sessions)
                .Where(e => e.InstructorId == instructorId)
                .ToListAsync();

        public async Task<Exam?> GetByIdAsync(int id)
            => await _context.Exams
                .Include(e => e.Instructor)
                .Include(e => e.Sessions)
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Exam?> GetWithQuestionsAsync(int id)
            => await _context.Exams
                .Include(e => e.Instructor)
                .Include(e => e.Questions.OrderBy(q => q.OrderIndex))
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Exam> CreateAsync(Exam exam)
        {
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task<Exam> UpdateAsync(Exam exam)
        {
            _context.Exams.Update(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task DeleteAsync(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
                await _context.SaveChangesAsync();
            }
        }
    }
}