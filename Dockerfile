FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["city-traffic-simulator-backend/city-traffic-simulator-backend.csproj", "city-traffic-simulator-backend/"]
RUN dotnet restore "city-traffic-simulator-backend/city-traffic-simulator-backend.csproj"
COPY . .
WORKDIR "/src/city-traffic-simulator-backend"
RUN dotnet build "city-traffic-simulator-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "city-traffic-simulator-backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "city-traffic-simulator-backend.dll"]
