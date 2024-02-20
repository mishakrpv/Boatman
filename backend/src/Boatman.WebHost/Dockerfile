FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Boatman.WebHost/Boatman.WebHost.csproj", "src/Boatman.WebHost/"]
COPY ["src/Controllers/Boatman.AuthApi.Controllers/Boatman.AuthApi.Controllers.csproj", "src/Controllers/Boatman.AuthApi.Controllers/"]
COPY ["src/UseCases/Boatman.AuthApi.UseCases/Boatman.AuthApi.UseCases.csproj", "src/UseCases/Boatman.AuthApi.UseCases/"]
COPY ["src/Interfaces/Boatman.DataAccess.Domain.Interfaces/Boatman.DataAccess.Domain.Interfaces.csproj", "src/Interfaces/Boatman.DataAccess.Domain.Interfaces/"]
COPY ["src/Entities/Boatman.Entities/Boatman.Entities.csproj", "src/Entities/Boatman.Entities/"]
COPY ["src/Interfaces/Boatman.DataAccess.Identity.Interfaces/Boatman.DataAccess.Identity.Interfaces.csproj", "src/Interfaces/Boatman.DataAccess.Identity.Interfaces/"]
COPY ["src/Utils/Boatman.Utils/Boatman.Utils.csproj", "src/Utils/Boatman.Utils/"]
COPY ["src/Controllers/Boatman.CustomerApi.Controllers/Boatman.CustomerApi.Controllers.csproj", "src/Controllers/Boatman.CustomerApi.Controllers/"]
COPY ["src/UseCases/Boatman.CustomerApi.UseCases/Boatman.CustomerApi.UseCases.csproj", "src/UseCases/Boatman.CustomerApi.UseCases/"]
COPY ["src/Controllers/Boatman.OwnerApi.Controllers/Boatman.OwnerApi.Controllers.csproj", "src/Controllers/Boatman.OwnerApi.Controllers/"]
COPY ["src/UseCases/Boatman.OwnerApi.UseCases/Boatman.OwnerApi.UseCases.csproj", "src/UseCases/Boatman.OwnerApi.UseCases/"]
COPY ["src/Implementations/Boatman.Emailing.Implementations/Boatman.Emailing.Implementations.csproj", "src/Implementations/Boatman.Emailing.Implementations/"]
COPY ["src/Interfaces/Boatman.Emailing.Interfaces/Boatman.Emailing.Interfaces.csproj", "src/Interfaces/Boatman.Emailing.Interfaces/"]
COPY ["src/Implementations/Boatman.Logging.Implementations/Boatman.Logging.Implementations.csproj", "src/Implementations/Boatman.Logging.Implementations/"]
COPY ["src/Interfaces/Boatman.Logging.Interfaces/Boatman.Logging.Interfaces.csproj", "src/Interfaces/Boatman.Logging.Interfaces/"]
COPY ["src/Implementations/Boatman.DataAccess.Domain.Implementations/Boatman.DataAccess.Domain.Implementations.csproj", "src/Implementations/Boatman.DataAccess.Domain.Implementations/"]
COPY ["src/Implementations/Boatman.DataAccess.Identity.Implementations/Boatman.DataAccess.Identity.Implementations.csproj", "src/Implementations/Boatman.DataAccess.Identity.Implementations/"]
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
