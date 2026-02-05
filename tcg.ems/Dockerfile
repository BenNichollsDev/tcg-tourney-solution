FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /src
EXPOSE 8080

# Copy solution & project files for caching
COPY ../tcg-tourney-solution.slnx ./
COPY TCG.EMS/*.csproj TCG.EMS/
COPY TCG.Domain/*.csproj TCG.Domain/
COPY TCG.Application/*.csproj TCG.Application/
COPY TCG.Infrastructure/*.csproj TCG.Infrastructure/
COPY TCG.Website/*.csproj TCG.Website/

# Hot reload command
CMD ["dotnet", "watch", "run", "--project", "TCG.EMS/TCG.EMS.csproj", "--urls", "http://0.0.0.0:8080"]