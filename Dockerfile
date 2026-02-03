FROM mcr.microsoft.com/dotnet/sdk:10.0 AS base
WORKDIR /src

# Expose ports
EXPOSE 8080
EXPOSE 8081

# Copy solution file first (for restore)
COPY tcg-tourney-solution.slnx ./

# Copy all csproj files (needed for multi-project restore caching)
COPY TCG.Website/*.csproj TCG.Website/
COPY TCG.EMS/*.csproj TCG.EMS/
COPY TCG.Domain/*.csproj TCG.Domain/
COPY TCG.Application/*.csproj TCG.Application/
COPY TCG.Infrastructure/*.csproj TCG.Infrastructure/

# Restore all NuGet packages
RUN dotnet restore tcg-tourney-solution.slnx --force

# Copy everything else
COPY . .

# Build (optional: skip restore since already done)
RUN dotnet build tcg-tourney-solution.slnx --no-restore -o /app/out