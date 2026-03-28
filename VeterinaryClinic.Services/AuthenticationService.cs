using System.Threading.Tasks;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;
using VeterinaryClinic.Shared.ViewModels;

namespace VeterinaryClinic.Services
{
    public class AuthenticationService
    {
        private readonly IUserService _userService;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<(bool success, string message)> RegisterUserAsync(RegisterViewModel model)
        {
            var dto = new RegisterUserDto
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Email
            };
            return await _userService.RegisterUserAsync(dto);
        }

        public async Task<bool> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userService.ValidateLoginAsync(model.Email, model.Password);
            return user != null;
        }
    }
}
