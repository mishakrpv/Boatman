using Boatman.AuthService.Interfaces.Dtos;
using Boatman.Utils.Models.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ResetPassword;

public class ResetPassword : IRequest<Response>
{
    public ResetPassword(ResetPasswordDto dto)
    {
        Dto = dto;
    }

    public ResetPasswordDto Dto { get; private set; }
}