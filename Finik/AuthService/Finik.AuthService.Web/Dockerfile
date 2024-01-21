#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthService/Finik.AuthService.Web/Finik.AuthService.Web.csproj", "AuthService/Finik.AuthService.Web/"]
COPY ["AuthService/Finik.AuthService.Contracts/Finik.AuthService.Contracts.csproj", "AuthService/Finik.AuthService.Contracts/"]
COPY ["AuthService/Finik.AuthService.Domain/Finik.AuthService.Domain.csproj", "AuthService/Finik.AuthService.Domain/"]
COPY ["AuthService/Finik.AuthService.DataAccess/Finik.AuthService.DataAccess.csproj", "AuthService/Finik.AuthService.DataAccess/"]
COPY ["AuthService/Finik.AuthService.EF/Finik.AuthService.EF.csproj", "AuthService/Finik.AuthService.EF/"]
COPY ["AuthService/Finik.AuthService.Services/Finik.AuthService.Services.csproj", "AuthService/Finik.AuthService.Services/"]
COPY ["AuthService/Finik.AuthService.Core/Finik.AuthService.Core.csproj", "AuthService/Finik.AuthService.Core/"]
RUN dotnet restore "./AuthService/Finik.AuthService.Web/./Finik.AuthService.Web.csproj"
COPY . .
WORKDIR "/src/AuthService/Finik.AuthService.Web"
RUN dotnet build "./Finik.AuthService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Finik.AuthService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finik.AuthService.Web.dll"]