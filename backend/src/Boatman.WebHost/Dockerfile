FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Boatman.WebHost/Boatman.WebHost.csproj", "src/Boatman.WebHost/"]
COPY ["src/Interfaces/Boatman.AuthService.Interfaces/Boatman.AuthService.Interfaces.csproj", "src/Interfaces/Boatman.AuthService.Interfaces/"]
COPY ["src/Utils/Boatman.Utils/Boatman.Utils.csproj", "src/Utils/Boatman.Utils/"]
COPY ["src/Controllers/Boatman.AuthApi.Controllers/Boatman.AuthApi.Controllers.csproj", "src/Controllers/Boatman.AuthApi.Controllers/"]
COPY ["src/UseCases/Boatman.AuthApi.UseCases/Boatman.AuthApi.UseCases.csproj", "src/UseCases/Boatman.AuthApi.UseCases/"]
COPY ["src/Implementations/Boatman.Emailing.Implementations.SendGrid/Boatman.Emailing.Implementations.SendGrid.csproj", "src/Implementations/Boatman.Emailing.Implementations.SendGrid/"]
COPY ["src/Interfaces/Boatman.Emailing.Interfaces/Boatman.Emailing.Interfaces.csproj", "src/Interfaces/Boatman.Emailing.Interfaces/"]
COPY ["src/Implementations/Boatman.Logging.Implementations/Boatman.Logging.Implementations.csproj", "src/Implementations/Boatman.Logging.Implementations/"]
COPY ["src/Interfaces/Boatman.Logging.Interfaces/Boatman.Logging.Interfaces.csproj", "src/Interfaces/Boatman.Logging.Interfaces/"]
COPY ["src/Interfaces/Boatman.DataAccess.Interfaces/Boatman.DataAccess.Interfaces.csproj", "src/Interfaces/Boatman.DataAccess.Interfaces/"]
COPY ["src/Entities/Boatman.Entities/Boatman.Entities.csproj", "src/Entities/Boatman.Entities/"]
COPY ["src/Controllers/Boatman.FrontendApi.Controllers/Boatman.FrontendApi.Controllers.csproj", "src/Controllers/Boatman.FrontendApi.Controllers/"]
COPY ["src/Interfaces/Boatman.BlobStorage.Interfaces/Boatman.BlobStorage.Interfaces.csproj", "src/Interfaces/Boatman.BlobStorage.Interfaces/"]
COPY ["src/UseCases/Boatman.FrontendApi.UseCases/Boatman.FrontendApi.UseCases.csproj", "src/UseCases/Boatman.FrontendApi.UseCases/"]
COPY ["src/Implementations/Boatman.DataAccess.Implementations.EntityFramework.Identity/Boatman.DataAccess.Implementations.EntityFramework.Identity.csproj", "src/Implementations/Boatman.DataAccess.Implementations.EntityFramework.Identity/"]
COPY ["src/Implementations/Boatman.AuthService.Implementations/Boatman.AuthService.Implementations.csproj", "src/Implementations/Boatman.AuthService.Implementations/"]
COPY ["src/Implementations/Boatman.BlobStorage.Implementations.AmazonS3/Boatman.BlobStorage.Implementations.AmazonS3.csproj", "src/Implementations/Boatman.BlobStorage.Implementations.AmazonS3/"]
RUN dotnet restore "src/Boatman.WebHost/Boatman.WebHost.csproj"
COPY . .
WORKDIR "/src/src/Boatman.WebHost"
RUN dotnet build "Boatman.WebHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Boatman.WebHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Boatman.WebHost.dll"]
