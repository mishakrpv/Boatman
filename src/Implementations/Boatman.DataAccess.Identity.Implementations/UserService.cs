using System.Text;
using Boatman.DataAccess.Identity.Interfaces;
using Boatman.DataAccess.Identity.Interfaces.Dtos;
using Boatman.Emailing.Interfaces;
using Boatman.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace Boatman.DataAccess.Identity.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;

    public UserService(UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IEmailSender emailSender) 
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailSender = emailSender; 
    }

    public async Task<Response<string>> RegisterUserAsync(RegisterDto dto)
    {
        if (dto.Password != dto.ConfirmPassword)
            return new Response<string>()
            {
                StatusCode = 400,
                Message = "Confirm password doesn't match the password."
            };

        var user = new ApplicationUser()
        {
            Email = dto.Email,
            UserName = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return new Response<string>()
            {
                StatusCode = 500,
                Message = "User was not created.",
                Errors = result.Errors.Select(e => e.Description)
            };
        
        // var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //
        // var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
        // var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
        //
        // string url = $"{_configuration["AppUrl"]}/api/auth/confirmemail?userid={user.Id}&token={validEmailToken}";
        //
        // await _mailService.SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
        //     $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
        //
        
        return new Response<string>(user.Id)
        {
            StatusCode = 200,
            Message = "User created successfully.",
        };
    }
}