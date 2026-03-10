using ExamGuard.API.DTOs;
using ExamGuard.API.Models;
using ExamGuard.API.Repositories.Interfaces;
using ExamGuard.API.Services.Interfaces;

namespace ExamGuard.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtService _jwtService;

        public AuthService(IUserRepository userRepo, JwtService jwtService)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);

            if (user == null || !user.IsActive)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            // Check role matches
            if (!string.IsNullOrEmpty(request.Role) &&
                !user.Role.Equals(request.Role, StringComparison.OrdinalIgnoreCase))
                return null;

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(user),
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                }
            };
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request)
        {
            if (await _userRepo.ExistsAsync(request.Email))
                return null;

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            await _userRepo.CreateAsync(user);

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(user),
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                }
            };
        }
    }
}