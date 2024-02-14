using Boatman.AuthApi.UseCases.Dtos;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.SignUpAsOwner;

public class SignUpAsOwnerRequest : IRequest<bool>
{
    public SignUpAsOwnerRequest(SignUpAsOwnerDto dto)
    {
        Dto = dto;
    }

    public SignUpAsOwnerDto Dto { get; private set; }
}