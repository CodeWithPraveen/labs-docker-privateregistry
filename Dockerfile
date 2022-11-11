# base stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BrezyWeather.csproj", "."]
RUN dotnet restore "./BrezyWeather.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BrezyWeather.csproj" -c Release -o /app/build

# publish stage
FROM build AS publish
RUN dotnet publish "BrezyWeather.csproj" -c Release -o /app/publish /p:UseAppHost=false

# final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrezyWeather.dll"]
