# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY WebApplication02.csproj .
RUN dotnet restore WebApplication02.csproj

# Copy remaining source code
COPY . .

# Build application
RUN dotnet build WebApplication02.csproj -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish WebApplication02.csproj -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication02.dll"]
