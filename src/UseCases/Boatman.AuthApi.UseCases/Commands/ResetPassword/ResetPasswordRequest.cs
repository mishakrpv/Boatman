﻿using Boatman.DataAccess.Identity.Interfaces.Dtos;
using Boatman.Utils;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ResetPassword;

public class ResetPasswordRequest : IRequest<Response>
{
    public ResetPasswordRequest(ResetPasswordDto dto)
    {
        Dto = dto;
    }

    public ResetPasswordDto Dto { get; private set; }
}