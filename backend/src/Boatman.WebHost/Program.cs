using System.Text;
using Boatman.AuthApi.Controllers;
using Boatman.AuthApi.UseCases.Commands.Register;
using Boatman.AuthService.Implementations.Jwt.Identity;
using Boatman.BlobStorage.Implementations.AmazonS3;
using Boatman.DataAccess.Implementations.EntityFramework.Identity;
using Boatman.FrontendApi.Catalog.Controllers;
using Boatman.FrontendApi.Customer.Controllers;
using Boatman.FrontendApi.Customer.UseCases.Commands.AddToFavorites;
using Boatman.FrontendApi.Owner.Controllers;
using Boatman.FrontendApi.Owner.UseCases.Commands.AddApartment;
using Boatman.WebHost.Configurations;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Logging.AddConsole();

builder.Configuration.AddEnvironmentVariables();

if ((builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Docker")
    && bool.Parse(config["UseInMemoryDb"] ?? "false"))
{
    builder.Services.AddDbContext<ApplicationContext>(o =>
        o.UseInMemoryDatabase("ApplicationDb"));
}
else
{
    builder.Services.AddDbContext<ApplicationContext>(options =>
    {
        var connectionString = config.GetConnectionString("ApplicationConnection");
        options.UseNpgsql(connectionString, o => o.EnableRetryOnFailure());
    });
}

builder.Services.AddHealthChecks()
    .AddNpgSql(config.GetConnectionString("ApplicationConnection") ?? "", name: "DbCheck");
//.AddRedis(config["RedisCS"] ?? "");

builder.Services.AddControllers()
    .AddApplicationPart(typeof(ApartmentController).Assembly)
    .AddApplicationPart(typeof(FavoritesController).Assembly)
    .AddApplicationPart(typeof(CatalogController).Assembly)
    .AddApplicationPart(typeof(AuthController).Assembly);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true
    };
});
//.AddOAuth();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;

    // SignIn settings
    options.SignIn.RequireConfirmedEmail = true;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddAuthorizationBuilder()
    .AddDefaultPolicy("Default", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
    })
    .AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
    });

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(
        typeof(AddApartmentHandler).Assembly,
        typeof(AddToFavoritesHandler).Assembly,
        typeof(RegisterHandler).Assembly);
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "0.9.1",
        Title = "Boatman",
        Description = "Platform for publishing apartments for rent from owners",
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/license/mit/")
        }
    });
});

builder.Services.AddInterfaceAdapters();
builder.Services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
builder.Services.Configure<AwsCredentials>(config.GetSection("AwsCredentials"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var applicationContext = scopedProvider.GetRequiredService<ApplicationContext>();
        await ApplicationContextSeed.SeedAsync(applicationContext, userManager, roleManager, config);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.Message);
    }
}

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/_health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();