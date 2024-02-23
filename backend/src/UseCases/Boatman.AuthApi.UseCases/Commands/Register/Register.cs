using Boatman.AuthService.Interfaces.Dtos;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.Register;

public class Register : IRequest<Response>
{
    public Register(RegisterDto dto)
    {
        Dto = dto;
    }

    public RegisterDto Dto { get; private set; }
}