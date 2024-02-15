using Boatman.DataAccess.Identity.Interfaces.Dtos;
using Boatman.Utils;

namespace Boatman.DataAccess.Identity.Interfaces;

public interface IUserService
{
    Task<Response<string>> RegisterUserAsync(RegisterDto dto);

    Task<Response<TokenResponse>> LoginUserAsync(LoginDto dto);

    Task<Response> ConfirmEmailAsync(string userId, string token);
}