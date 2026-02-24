# Use .NET SDK for building & hot reload
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy solution and all project files for restore caching
COPY *.sln .
COPY TCG.EMS/*.csproj ./TCG.EMS/
COPY TCG.Website/*.csproj ./TCG.Website/
COPY TCG.Application/*.csproj ./TCG.Application/
COPY TCG.Infrastructure/*.csproj ./TCG.Infrastructure/
COPY TCG.Domain/*.csproj ./TCG.Domain/

# Restore solution (all projects must exist)
RUN dotnet restore tcg-tourney.sln

# Copy the rest of the source code
COPY . .

# Clean bin/obj to prevent file lock issues in Docker
RUN dotnet clean tcg-tourney.sln

# Expose port for EMS
EXPOSE 8080

# Entrypoint: run dotnet watch with hot reload enabled
ENTRYPOINT ["dotnet", "watch", "--project", "TCG.EMS/TCG.EMS.csproj", "run", "--urls", "http://0.0.0.0:8080"]