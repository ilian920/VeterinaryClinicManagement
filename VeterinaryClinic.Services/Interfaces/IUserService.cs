using VeterinaryClinic.Services.DTOs;

namespace VeterinaryClinic.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> GetUserByUsernameAsync(string username);
    Task<(bool success, string message)> RegisterUserAsync(RegisterUserDto dto);
    Task<UserDto?> ValidateLoginAsync(string username, string password);
    Task<bool> UpdateUserAsync(UserDto dto);
    Task<bool> DeleteUserAsync(int id);
}
