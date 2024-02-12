using Boatman.AuthApi.UseCases.Dtos;
using Boatman.TokenService.Interfaces;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.SignUpAsOwner;

public class SignUpAsOwnerRequest : IRequest<bool>
{
    public SignUpAsOwnerDto Dto { get; private set; }

    public SignUpAsOwnerRequest(SignUpAsOwnerDto dto)
    {
        Dto = dto;
    }
}