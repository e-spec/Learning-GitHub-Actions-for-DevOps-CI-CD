# ============================================================
# What is .NET?
# .NET is a development platform created by Microsoft
# for building web, desktop, cloud, mobile, and API applications.
#
# What is ASP.NET?
# ASP.NET is a web framework built on .NET used to create
# web applications and REST APIs.
#
# What is the .NET SDK?
# SDK (Software Development Kit) includes the compiler,
# build tools, and CLI commands (dotnet restore, build, publish).
# It is required to BUILD the application.
#
# What is the ASP.NET Runtime Image?
# It contains only what is needed to RUN the application.
# It does not include build tools (smaller and production-ready).
#
# What is NuGet?
# NuGet is the package manager for .NET (like pip or npm).
# It downloads external libraries defined in the .csproj file.
# When we run "dotnet restore", NuGet fetches those dependencies.
# ============================================================


# -----------------------------
# Stage 1: Base runtime image
# -----------------------------
# Uses ASP.NET runtime image (only needed to RUN the app)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

WORKDIR /app

# Exposes HTTP (80) and HTTPS (443)
EXPOSE 80
EXPOSE 443


# -----------------------------
# Stage 2: Build stage
# -----------------------------
# Uses full .NET SDK (needed to build/compile the project)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy project file first (improves Docker layer caching)
COPY WebApplication02/WebApplication02.csproj WebApplication02/

# Restore dependencies using NuGet
# NuGet reads the .csproj file and downloads required packages
RUN dotnet restore WebApplication02/WebApplication02.csproj

# Copy remaining source code
COPY . .

WORKDIR /src/WebApplication02

# Build the project in Release mode
RUN dotnet build WebApplication02.csproj -c Release -o /app/build


# -----------------------------
# Stage 3: Publish stage
# -----------------------------
# Publish creates optimized deployment files
FROM build AS publish

RUN dotnet publish WebApplication02.csproj -c Release -o /app/publish


# -----------------------------
# Stage 4: Final runtime image
# -----------------------------
# Start from lightweight runtime (no SDK included)
FROM base AS final

WORKDIR /app

# Copy only published files (no source code, no SDK)
COPY --from=publish /app/publish .

# Command executed when container starts
ENTRYPOINT ["dotnet", "WebApplication02.dll"]
