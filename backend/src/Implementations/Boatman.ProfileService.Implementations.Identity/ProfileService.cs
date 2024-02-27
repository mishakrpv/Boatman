using System.Xml;
using Boatman.DataAccess.Implementations.EntityFramework.Identity;
using Boatman.ProfileService.Interfaces;
using Boatman.ProfileService.Interfaces.Dtos;
using Boatman.Utils.Models.Response;
using Microsoft.AspNetCore.Identity;

namespace Boatman.ProfileService.Implementations.Identity;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Response> EditProfile(PersonalDataWithPrincipalDto dto)
    {
        var response = new Response();
        var user = await _userManager.GetUserAsync(dto.Principal);

        if (user == null)
        {
            response.StatusCode = 404;
            response.Message = "User not found.";
        }
        else
        {
            user.FirstName = dto.FirstName;
            user.MiddleName = dto.MiddleName;
            user.LastName = dto.LastName;
            user.Bio = dto.Bio;

            await _userManager.UpdateAsync(user);

            response.Message = "Profile has been edited.";
        }

        return response;
    }
}