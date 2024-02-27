using Boatman.ProfileService.Interfaces.Dtos;
using Boatman.Utils.Models.Response;

namespace Boatman.ProfileService.Interfaces;

public interface IProfileService
{
    Task<Response> EditProfile(PersonalDataWithPrincipalDto dto);
}