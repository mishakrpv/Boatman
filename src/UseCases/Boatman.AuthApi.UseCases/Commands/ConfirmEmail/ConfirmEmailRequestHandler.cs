﻿using Boatman.DataAccess.Identity.Interfaces;
using Boatman.Utils;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.ConfirmEmail;

public class ConfirmEmailRequestHandler : IRequestHandler<ConfirmEmailRequest, Response>
{
    private readonly IUserService _userService;

    public ConfirmEmailRequestHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<Response> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var response = await _userService.ConfirmEmailAsync(request.Dto.UserId, request.Dto.Token);

        return response;
    }
}