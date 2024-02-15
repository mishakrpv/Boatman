using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Boatman.DataAccess.Identity.Interfaces;
using Boatman.DataAccess.Identity.Interfaces.Dtos;
using Boatman.Emailing.Interfaces;
using Boatman.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Boatman.DataAccess.Identity.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSettings _settings;
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;

    public UserService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> settings,
        IConfiguration configuration,
        IEmailSender emailSender) 
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailSender = emailSender;
        _settings = settings.Value;
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
                StatusCode = 400,
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
        // await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
        //     $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
        
        
        return new Response<string>(user.Id)
        {
            Message = "User created successfully.",
        };
    }

    public async Task<Response<TokenResponse>> LoginUserAsync(LoginDto dto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_settings.Key);
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return new Response<TokenResponse>()
            {
                StatusCode = 404,
                Message = "There is no user with that Email address."
            };

        var result = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!result)
            return new Response<TokenResponse>()
            {
                StatusCode = 401,
                Message = "Invalid password."
            };
        
        var roles = await _userManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, dto.Email)
        };
        
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(_settings.ExpiresInDays).AddMinutes(_settings.ExpiresInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
        };
        
        var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

        var tokenResponse = new TokenResponse()
        {
            Token = token,
            Expires = tokenDescriptor.Expires
        };
        
        return new Response<TokenResponse>(tokenResponse)
        {
            Message = "You have successfully logged in."
        };
    }

    public Task<Response> ConfirmEmailAsync(string userId, string token)
    {
        throw new NotImplementedException();
    }
}