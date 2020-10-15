FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["geocode/Application/Application.csproj", "geocode/Application/"]
COPY ["geocode/Domain/Domain.csproj", "geocode/Domain/"]
COPY ["geocode/Services/Services.csproj", "geocode/Services/"]
RUN dotnet restore "geocode\Application\Application.csproj"
COPY . .
WORKDIR "/src/geocode/Application"
RUN dotnet build "Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Application.dll"]