FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /src
EXPOSE 8080

COPY ../tcg-tourney.slnx ./
COPY TCG.EMS/*.csproj TCG.EMS/
COPY TCG.Domain/*.csproj TCG.Domain/
COPY TCG.Application/*.csproj TCG.Application/
COPY TCG.Infrastructure/*.csproj TCG.Infrastructure/

CMD ["dotnet", "watch", "run", "--project", "TCG.EMS/TCG.EMS.csproj", "--urls", "http://0.0.0.0:8080"]