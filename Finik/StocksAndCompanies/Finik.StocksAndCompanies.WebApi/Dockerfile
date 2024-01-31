#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StocksAndCompanies/Finik.StocksAndCompanies.WebApi/Finik.StocksAndCompanies.WebApi.csproj", "StocksAndCompanies/Finik.StocksAndCompanies.WebApi/"]
COPY ["StocksAndCompanies/Finik.StocksAndCompanies.EfData/Finik.StocksAndCompanies.EfData.csproj", "StocksAndCompanies/Finik.StocksAndCompanies.EfData/"]
COPY ["StocksAndCompanies/Finik.StocksAndСompanies.Core/Finik.StocksAndСompanies.Core.csproj", "StocksAndCompanies/Finik.StocksAndСompanies.Core/"]
RUN dotnet restore "./StocksAndCompanies/Finik.StocksAndCompanies.WebApi/./Finik.StocksAndCompanies.WebApi.csproj"
COPY . .
WORKDIR "/src/StocksAndCompanies/Finik.StocksAndCompanies.WebApi"
RUN dotnet build "./Finik.StocksAndCompanies.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Finik.StocksAndCompanies.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finik.StocksAndCompanies.WebApi.dll"]