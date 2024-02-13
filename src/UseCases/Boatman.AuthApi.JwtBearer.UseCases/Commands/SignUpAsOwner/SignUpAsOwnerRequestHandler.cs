using Ardalis.GuardClauses;
using Boatman.DataAccess.Domain.Interfaces;
using Boatman.DataAccess.Identity.Interfaces;
using Boatman.Entities.Models.OwnerAggregate;
using Boatman.TokenService.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boatman.AuthApi.UseCases.Commands.SignUpAsOwner;

public class SignUpAsOwnerRequestHandler : IRequestHandler<SignUpAsOwnerRequest, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IPasswordManager _passManager;
    private readonly IRepository<Owner> _ownerRepo;

    public SignUpAsOwnerRequestHandler(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IPasswordManager passManager,
        IRepository<Owner> ownerRepo)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _passManager = passManager;
        _ownerRepo = ownerRepo;
    }
        
    public async Task<bool> Handle(SignUpAsOwnerRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var userExists = await _userManager.FindByEmailAsync(dto.Email);
        if (userExists != null)
            return false;
        
        // TODO: Check is it better to generate passwordHash yourself or is it better to delegate it to UserManager
        string salt = _passManager.GetNewSalt();
        string passwordHash = _passManager.GetPasswordHash(dto.Password, salt);
        var user = new ApplicationUser()
        {
            Email = dto.Email,
            SecurityStamp = new Guid().ToString(),
            PasswordHash = passwordHash,
            Salt = salt,
            PhoneNumber = dto.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
            return false;
        
        if (!await _roleManager.RoleExistsAsync(nameof(Owner)))
            await _roleManager.CreateAsync(new IdentityRole(nameof(Owner)));
        
        if (await _roleManager.RoleExistsAsync(nameof(Owner)))
        {
            await _userManager.AddToRoleAsync(user, nameof(Owner));
        }
        
        await _ownerRepo.AddAsync(new Owner(user.Id)
        {
            FirstName = request.Dto.FirstName,
            MiddleName = request.Dto.MiddleName,
            LastName = request.Dto.LastName,
            Bio = request.Dto.Bio
        });

        return true;
    }
}