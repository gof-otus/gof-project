#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Finik.MainPage.Web/Finik.MainPage.Web.csproj", "Finik.MainPage.Web/"]
COPY ["Finik.MainPage.Core/Finik.MainPage.Core.csproj", "Finik.MainPage.Core/"]
COPY ["Finik.MainPage.Infrastructure/Finik.MainPage.Infrastructure.csproj", "Finik.MainPage.Infrastructure/"]
COPY ["NewsService/Finik.NewsService.Infrastructure/Finik.NewsService.Infrastructure.csproj", "NewsService/Finik.NewsService.Infrastructure/"]
COPY ["NewsService/Finik.NewsService.Contracts/Finik.NewsService.Contracts.csproj", "NewsService/Finik.NewsService.Contracts/"]
COPY ["NewsService/Finik.NewsService.Core/Finik.NewsService.Core.csproj", "NewsService/Finik.NewsService.Core/"]
COPY ["NewsService/Finik.NewsService.DbData/Finik.NewsService.DbData.csproj", "NewsService/Finik.NewsService.DbData/"]
COPY ["NewsService/Finik.NewsService.Models/Finik.NewsService.Models.csproj", "NewsService/Finik.NewsService.Models/"]
RUN dotnet restore "Finik.MainPage.Web/Finik.MainPage.Web.csproj"
COPY . .
WORKDIR "/src/Finik.MainPage.Web"
RUN dotnet build "Finik.MainPage.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Finik.MainPage.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finik.MainPage.Web.dll"]