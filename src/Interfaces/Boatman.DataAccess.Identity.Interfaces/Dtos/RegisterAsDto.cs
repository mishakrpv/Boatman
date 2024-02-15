namespace Boatman.DataAccess.Identity.Interfaces.Dtos;

public class RegisterAsDto : RegisterDto
{
    public RegisterAsDto(string role)
    {
        Role = role;
    }

    public string Role { get; private set; }
}