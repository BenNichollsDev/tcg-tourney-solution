FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /src
EXPOSE 8080

COPY . .

RUN dotnet restore tcg-tourney.sln

CMD ["dotnet", "watch", "run", "--project", "TCG.EMS/TCG.EMS.csproj", "--urls", "http://0.0.0.0:8080"]