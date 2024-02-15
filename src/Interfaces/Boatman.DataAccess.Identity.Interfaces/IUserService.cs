using Boatman.DataAccess.Identity.Interfaces.Dtos;
using Boatman.Utils;

namespace Boatman.DataAccess.Identity.Interfaces;

public interface IUserService
{
    Task<Response<string>> RegisterUserAsync(RegisterDto dto);
}