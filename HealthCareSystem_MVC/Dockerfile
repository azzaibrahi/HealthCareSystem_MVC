# Multi-stage Dockerfile adapted from the MVC reference to the three-layer Health Care solution.
# Build context: the repository root (D:\Rwad_MVC\HealthCareSystem_MVC).

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HealthCareSystem_MVC.DAL/HealthCareSystem_MVC.DAL.csproj", "HealthCareSystem_MVC.DAL/"]
COPY ["HealthCareSystem_MVC.BLL/HealthCareSystem_MVC.BLL.csproj", "HealthCareSystem_MVC.BLL/"]
COPY ["HealthCareSystem_MVC/HealthCareSystem_MVC.csproj", "HealthCareSystem_MVC/"]
RUN dotnet restore "./HealthCareSystem_MVC/HealthCareSystem_MVC.csproj"
COPY . .
WORKDIR "/src/HealthCareSystem_MVC"
RUN dotnet build "./HealthCareSystem_MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HealthCareSystem_MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthCareSystem_MVC.dll"]
