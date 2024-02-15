using Boatman.AuthApi.UseCases.Dtos;
using Boatman.Utils;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.RegisterAsOwner;

public class RegisterAsOwnerRequest : IRequest<Response>
{
    public RegisterAsOwnerDto Dto { get; private set; }

    public RegisterAsOwnerRequest(RegisterAsOwnerDto dto)
    {
        Dto = dto;
    }
}