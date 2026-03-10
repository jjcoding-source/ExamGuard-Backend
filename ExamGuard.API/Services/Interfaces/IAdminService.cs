using ExamGuard.API.DTOs;

namespace ExamGuard.API.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(int id);
        Task<UserResponseDto> CreateUserAsync(CreateUserDto dto);
        Task<UserResponseDto?> UpdateUserAsync(int id, CreateUserDto dto);
        Task<bool> ToggleUserAsync(int id);
    }
}