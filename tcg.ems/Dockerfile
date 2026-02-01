FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TCG.Domain/TCG.Domain.csproj", "TCG.Domain/"]
COPY ["TCG.Application/TCG.Application.csproj", "TCG.Application/"]
COPY ["TCG.Infrastructure/TCG.Infrastructure.csproj", "TCG.Infrastructure/"]
COPY ["TCG.EMS/TCG.EMS.csproj", "TCG.EMS/"]
RUN dotnet restore "TCG.EMS/TCG.EMS.csproj"
COPY . .
WORKDIR "/src/TCG.EMS"
RUN dotnet build "TCG.EMS.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "TCG.EMS.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TCG.EMS.dll"]
