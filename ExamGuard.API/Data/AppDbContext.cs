using ExamGuard.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ExamGuard.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ExamSession> ExamSessions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ProctorEvent> ProctorEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Role).HasMaxLength(20);
            });

            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Instructor)
                .WithMany(u => u.Exams)
                .HasForeignKey(e => e.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExamSession>()
                .HasOne(s => s.Exam)
                .WithMany(e => e.Sessions)
                .HasForeignKey(s => s.ExamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExamSession>()
                .HasOne(s => s.Student)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Session)
                .WithMany(s => s.Answers)
                .HasForeignKey(a => a.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProctorEvent>()
                .HasOne(p => p.Session)
                .WithMany(s => s.ProctorEvents)
                .HasForeignKey(p => p.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}