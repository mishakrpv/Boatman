using System.Text;
using Boatman.DataAccess.Domain.Implementations;
using Boatman.DataAccess.Identity.Implementations;
using Boatman.Entities.Models.CustomerAggregate;
using Boatman.Entities.Models.OwnerAggregate;
using Boatman.TokenService.Implementations;
using Boatman.OwnerApi.Controllers;
using Boatman.OwnerApi.UseCases.Commands.AddApartment;
using Boatman.WebHost.Configurations;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Logging.AddConsole();

builder.Configuration.AddEnvironmentVariables();

if ((builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Docker")
    && bool.Parse(config["UseInMemoryDb"] ?? "false"))
{
    builder.Services.AddDbContext<DomainContext>(o =>
        o.UseInMemoryDatabase("DomainDb"));
    builder.Services.AddDbContext<IdentityContext>(o =>
        o.UseInMemoryDatabase("IdentityDb"));
}
else
{
    builder.Services.AddDbContext<DomainContext>(options =>
    {
        var connectionString = config.GetConnectionString("DomainConnection");
        options.UseSqlServer(connectionString, o => o.EnableRetryOnFailure());
    });
    builder.Services.AddDbContext<IdentityContext>(options =>
    {
        var connectionString = config.GetConnectionString("IdentityConnection");
        options.UseSqlServer(connectionString, o => o.EnableRetryOnFailure());
    });
}

builder.Services.AddHealthChecks()
    .AddSqlServer(config.GetConnectionString("DomainConnection") ?? "", name: "domainCheck")
    .AddSqlServer(config.GetConnectionString("IdentityConnection") ?? "", name: "identityCheck");
    //.AddRedis(config["RedisCS"] ?? "");

builder.Services.AddControllers()
    .AddApplicationPart(typeof(ApartmentController).Assembly)
    .AddApplicationPart(typeof(AccountController).Assembly)
    .AddApplicationPart(typeof(AuthController).Assembly);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});
//.AddOAuth();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(nameof(Owner), policy =>
    {
        policy.RequireRole(nameof(Owner));
    });
    options.AddPolicy(nameof(Customer), policy =>
    {
        policy.RequireRole(nameof(Customer));
    });
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
    });
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(SignUpAsOwnerRequestHandler).Assembly,
        typeof(AddApartmentRequestHandler).Assembly)
});

builder.Services.AddInterfaceAdapters();

var app = builder.Build();

app.MapHealthChecks("/_health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();