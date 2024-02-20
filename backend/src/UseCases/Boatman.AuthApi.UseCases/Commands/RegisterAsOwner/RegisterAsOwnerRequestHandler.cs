﻿using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Identity.Interfaces;
using Boatman.DataAccess.Identity.Interfaces.Dtos;
using Boatman.Entities.Models.OwnerAggregate;
using Boatman.Utils;
using Boatman.Utils.Response;
using MediatR;

namespace Boatman.AuthApi.UseCases.Commands.RegisterAsOwner;

public class RegisterAsOwnerRequestHandler : IRequestHandler<RegisterAsOwnerRequest, Response>
{
    private readonly IUserService _userService;
    private readonly IRepository<Owner> _ownerRepo;

    public RegisterAsOwnerRequestHandler(IUserService userService, IRepository<Owner> ownerRepo)
    {
        _userService = userService;
        _ownerRepo = ownerRepo;
    }

    public async Task<Response> Handle(RegisterAsOwnerRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var response = await _userService.RegisterUserAsync(new RegisterAsDto(nameof(Owner))
        {
            Email = dto.Email,
            Password = dto.Password,
            ConfirmPassword = dto.ConfirmPassword
        });

        if (response.StatusCode != 200)
            return response;

        var owner = await _ownerRepo.AddAsync(new Owner(response.Value!)
        {
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            Bio = dto.Bio
        }, default);

        return response;
    }
}