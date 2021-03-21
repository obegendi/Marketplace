#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Marketplace.API/Marketplace.API.csproj", "src/Marketplace.API/"]
COPY ["src/Data/Marketplace.Data/Marketplace.Data.csproj", "src/Data/Marketplace.Data/"]
COPY ["src/Domain/Marketplace.Domain/Marketplace.Domain.csproj", "src/Domain/Marketplace.Domain/"]
COPY ["src/Common/Marketplace.Common/Marketplace.Common.csproj", "src/Common/Marketplace.Common/"]
COPY ["src/Infrastructure/Marketplace.API.Infrastructure/Marketplace.API.Infrastructure.csproj", "src/Infrastructure/Marketplace.API.Infrastructure/"]
COPY ["src/Application/Marketplace.Application.LocationServices/Marketplace.Application.LocationServices.csproj", "src/Application/Marketplace.Application.LocationServices/"]
COPY ["src/Application/Marketplace.Application.ProductServices/Marketplace.Application.ProductServices.csproj", "src/Application/Marketplace.Application.ProductServices/"]
COPY ["src/Application/Marketplace.Application.AuthenticationServices/Marketplace.Application.AuthenticationServices.csproj", "src/Application/Marketplace.Application.AuthenticationServices/"]
COPY ["src/Application/Marketplace.Application.OrderServices/Marketplace.Application.OrderServices.csproj", "src/Application/Marketplace.Application.OrderServices/"]
COPY ["src/Application/Marketplace.Application.CustomerServices/Marketplace.Application.CustomerServices.csproj", "src/Application/Marketplace.Application.CustomerServices/"]
COPY ["src/Application/Marketplace.Application.PartnerServices/Marketplace.Application.MerchantServices.csproj", "src/Application/Marketplace.Application.PartnerServices/"]
RUN dotnet restore "src/Marketplace.API/Marketplace.API.csproj"
COPY . .
WORKDIR "/src/src/Marketplace.API"
RUN dotnet build "Marketplace.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Marketplace.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Marketplace.API.dll"]