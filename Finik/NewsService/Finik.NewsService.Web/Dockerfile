#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NewsService/Finik.NewsService.Web/Finik.NewsService.Web.csproj", "NewsService/Finik.NewsService.Web/"]
COPY ["NewsService/Finik.NewsService.Core/Finik.NewsService.Core.csproj", "NewsService/Finik.NewsService.Core/"]
COPY ["NewsService/Finik.NewsService.Contracts/Finik.NewsService.Contracts.csproj", "NewsService/Finik.NewsService.Contracts/"]
COPY ["NewsService/Finik.NewsService.EfData/Finik.NewsService.EfData.csproj", "NewsService/Finik.NewsService.EfData/"]
COPY ["NewsService/Finik.NewsService.DbData/Finik.NewsService.DbData.csproj", "NewsService/Finik.NewsService.DbData/"]
COPY ["NewsService/Finik.NewsService.Models/Finik.NewsService.Models.csproj", "NewsService/Finik.NewsService.Models/"]
COPY ["NewsService/Finik.NewsService.Infrastructure/Finik.NewsService.Infrastructure.csproj", "NewsService/Finik.NewsService.Infrastructure/"]
RUN dotnet restore "./NewsService/Finik.NewsService.Web/./Finik.NewsService.Web.csproj"
COPY . .
WORKDIR "/src/NewsService/Finik.NewsService.Web"
RUN dotnet build "./Finik.NewsService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Finik.NewsService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finik.NewsService.Web.dll"]