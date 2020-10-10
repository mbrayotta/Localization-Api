FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["geolocator/API/API.csproj", "geolocator/API/"]
COPY ["geolocator/Domain/Domain.csproj", "geolocator/Domain/"]
COPY ["geolocator/Persistence/Persistence.csproj", "geolocator/Persistence/"]
COPY ["geolocator/Services/Services.csproj", "geolocator/Services/"]
RUN dotnet restore "geolocator\API\API.csproj"
COPY . .
WORKDIR "/src/geolocator/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
