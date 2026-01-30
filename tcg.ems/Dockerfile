# References:
# [1]: Scriptbytes (2024) Blazor 8 Docker | Build a Blog - Ep 04.5 [Source Code]. Available at: https://www.youtube.com/watch?v=lDXdk9dS1lM [Accessed 26/12/2025].

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

#<-*****Scriptbytes (2024) [1] - START

WORKDIR /src

COPY . .

#<-*****Scriptbytes (2024) [1] - END

RUN dotnet restore fothebys.sln
RUN dotnet build fothebys.sln -c Release -o /app/build

#<-*****Scriptbytes (2024) [1] - START

FROM build AS publish

#<-*****Scriptbytes (2024) [1] - END

RUN dotnet publish Fothebys.RMS/Fothebys.RMS.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

#<-*****Scriptbytes (2024) [1] - START

WORKDIR /app

EXPOSE 8080
COPY --from=publish /app/publish .

#<-*****Scriptbytes (2024) [1] - END

ENTRYPOINT ["dotnet", "Fothebys.RMS.dll"]