FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BetAt.Api/BetAt.Api.csproj", "BetAt.Api/"]
COPY ["BetAt.Application/BetAt.Application.csproj", "BetAt.Application/"]
COPY ["BetAt.Domain/BetAt.Domain.csproj", "BetAt.Domain/"]
COPY ["BetAt.Infrastructure/BetAt.Infrastructure.csproj", "BetAt.Infrastructure/"]
RUN dotnet restore "BetAt.Api/BetAt.Api.csproj"
COPY . .
WORKDIR "/src/BetAt.Api"
RUN dotnet build "BetAt.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BetAt.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BetAt.Api.dll"]
```

Ajoute aussi un fichier `.dockerignore` à la racine :
```
**/bin/
**/obj/
**/.vs/
**/.vscode/
**/node_modules/
