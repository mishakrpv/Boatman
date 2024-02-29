using System.Security.Claims;
using Boatman.ProfileService.Interfaces.Dtos;
using Boatman.Utils.Models.Response;

namespace Boatman.ProfileService.Interfaces;

public interface IProfileService
{
    Task<Response> EditProfile(PersonalDataWithPrincipalDto dto);

    Task<Response> ChangeEmail(ClaimsPrincipal principal, string email);
}