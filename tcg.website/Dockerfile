# Base image
FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /src
EXPOSE 8081

# Copy only solution & project files for caching
COPY ../tcg-tourney-solution.slnx ./
COPY TCG.Website/*.csproj TCG.Website/
COPY TCG.Domain/*.csproj TCG.Domain/
COPY TCG.Application/*.csproj TCG.Application/
COPY TCG.Infrastructure/*.csproj TCG.Infrastructure/

# Don't run restore — we will use volume-mounted code
# Everything else will be mounted at runtime

# Hot reload command
CMD ["dotnet", "watch", "run", "--project", "TCG.Website/TCG.Website.csproj", "--urls", "http://0.0.0.0:8081"]