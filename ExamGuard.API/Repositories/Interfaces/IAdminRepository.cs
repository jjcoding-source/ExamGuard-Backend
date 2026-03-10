using ExamGuard.API.Models;

namespace ExamGuard.API.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> ToggleUserAsync(int id);
    }
}