using Boatman.AuthService.Interfaces.Dtos;
using Boatman.Utils.Models.Response;

namespace Boatman.AuthService.Interfaces;

public interface IAuthService
{
    Task<Response> RegisterUserAsync(RegisterDto dto);

    Task<Response<TokenDto>> LoginUserAsync(LoginDto dto);

    Task<Response> ConfirmEmailAsync(ConfirmEmailDto dto);

    Task<Response> ForgetPasswordAsync(string email);

    Task<Response> ResetPasswordAsync(ResetPasswordDto dto);
}