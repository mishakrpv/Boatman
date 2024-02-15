﻿using Boatman.DataAccess.Identity.Interfaces;
using Boatman.DataAccess.Identity.Interfaces.Dtos;
using Boatman.Utils;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.Login;

public class LoginRequest : IRequest<Response<TokenResponse>>
{
    public LoginDto Dto { get; private set; }
    
    public LoginRequest(LoginDto dto)
    {
        Dto = dto;
    }
}