using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Boatman.AuthService.Interfaces;
using Boatman.AuthService.Interfaces.Dtos;
using Boatman.DataAccess.Implementations.EntityFramework.Identity;
using Boatman.Emailing.Interfaces;
using Boatman.Utils.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Boatman.AuthService.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtSettings _settings;
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;
    
    public AuthService(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JwtSettings> settings,
        IConfiguration configuration,
        IEmailSender emailSender)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _settings = settings.Value;
        _configuration = configuration;
        _emailSender = emailSender;
    }

    public async Task<Response> RegisterUserAsync(RegisterDto dto)
    {
        if (dto.Password != dto.ConfirmPassword)
            return new Response<string>
            {
                StatusCode = 400,
                Message = "Confirm password doesn't match the password."
            };

        var user = new ApplicationUser
        {
            Email = dto.Email,
            UserName = dto.Email,
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            Bio = dto.Bio
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return new Response
            {
                StatusCode = 400,
                Message = "User was not created.",
                Errors = result.Errors.Select(e => e.Description)
            };

        var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        
        var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
        var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

        var callbackUrl = $"{_configuration["AppUrl"]}/auth/confirm-email?id={user.Id}&token={validEmailToken}";
        
        await _emailSender.SendEmailAsync(user.Email, "Confirm your email", "<h1>Welcome to Boatman</h1>" +
            $"<p>Please confirm your email by <a href='{callbackUrl}'>clicking here</a></p>");

        return new Response
        {
            Message = "User created successfully."
        };
    }

    public async Task<Response<TokenDto>> LoginUserAsync(LoginDto dto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_settings.Key);
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return new Response<TokenDto>
            {
                StatusCode = 404,
                Message = "There is no user with that Email address."
            };

        var result = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!result)
            return new Response<TokenDto>
            {
                StatusCode = 401,
                Message = "Invalid password."
            };

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, dto.Email)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(_settings.ExpiresInDays).AddMinutes(_settings.ExpiresInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

        var tokenDto = new TokenDto
        {
            AccessToken = token,
            Expires = tokenDescriptor.Expires
        };

        return new Response<TokenDto>(tokenDto)
        {
            Message = "You have successfully logged in."
        };
    }

    public async Task<Response> ConfirmEmailAsync(ConfirmEmailDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);
        
        if (user == null)
            return new Response
            {
                StatusCode = 404,
                Message = "User not found."
            };

        var decodedToken = WebEncoders.Base64UrlDecode(dto.Token);
        var confirmEmailToken = Encoding.UTF8.GetString(decodedToken);

        var result = await _userManager.ConfirmEmailAsync(user, confirmEmailToken);

        if (result.Succeeded)
            return new Response
            {
                Message = "Email confirmed successfully."
            };
        
        return new Response
        {
            StatusCode = 400,
            Message = "Email wasn't confirmed.",
            Errors = result.Errors.Select(e => e.Description)
        };
    }

    public async Task<Response> ForgetPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return new Response
            {
                StatusCode = 404,
                Message = "There is no user with that Email address."
            };

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedResetToken = Encoding.UTF8.GetBytes(resetToken);
        var validResetToken = WebEncoders.Base64UrlEncode(encodedResetToken);

        var resetUrl = $"{_configuration["AppUrl"]}/auth/reset-password?email={email}&token={validResetToken}";

        await _emailSender.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
            $"<p>To reset your password <a href='{resetUrl}'>click here</a></p>");

        return new Response
        {
            Message = "Reset password URL has been sent to the email."
        };
    }

    public async Task<Response> ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return new Response
            {
                StatusCode = 404,
                Message = "There is no user with that Email address."
            };
        
        if (dto.Password != dto.ConfirmPassword)
            return new Response
            {
                StatusCode = 400,
                Message = "Confirm password doesn't match the password."
            };

        var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Password);

        if (result.Succeeded)
            return new Response
            {
                Message = "Password has been reset.",
            };
        
        return new Response
        {
            StatusCode = 400,
            Message = "Password wasn't reset.",
            Errors = result.Errors.Select(e => e.Description)
        };
    }
}