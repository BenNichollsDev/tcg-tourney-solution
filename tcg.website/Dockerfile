FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /src
EXPOSE 8081

COPY ../tcg-tourney-solution.slnx ./
COPY TCG.Website/*.csproj TCG.Website/
COPY TCG.Domain/*.csproj TCG.Domain/
COPY TCG.Application/*.csproj TCG.Application/
COPY TCG.Infrastructure/*.csproj TCG.Infrastructure/

CMD ["dotnet", "run", "--project", "TCG.Website/TCG.Website.csproj", "--urls", "http://0.0.0.0:8081"]