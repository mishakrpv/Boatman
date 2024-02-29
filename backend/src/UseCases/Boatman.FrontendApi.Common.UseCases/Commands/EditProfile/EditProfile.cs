using Boatman.ProfileService.Interfaces.Dtos;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.FrontendApi.Common.UseCases.Commands.EditProfile;

public class EditProfile : IRequest<Response>
{
    public EditProfile(PersonalDataWithPrincipalDto dto)
    {
        Dto = dto;
    }

    public PersonalDataWithPrincipalDto Dto { get; private set; }
}