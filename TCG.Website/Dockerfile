FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /src
EXPOSE 8081

COPY . .

RUN dotnet restore tcg-tourney.sln

CMD ["dotnet", "run", "--project", "TCG.Website/TCG.Website.csproj", "--urls", "http://0.0.0.0:8081"]