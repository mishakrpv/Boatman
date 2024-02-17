using Boatman.AuthApi.UseCases.Dtos;
using Boatman.Utils;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ConfirmEmail;

public class ConfirmEmailRequest : IRequest<Response>
{
    public ConfirmEmailRequest(ConfirmEmailDto dto)
    {
        Dto = dto;
    }

    public ConfirmEmailDto Dto { get; private set; }
}