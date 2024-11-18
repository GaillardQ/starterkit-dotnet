FROM mcr.microsoft.com/dotnet/sdk:8.0 as publish

WORKDIR /src
COPY ["src/", "."]

RUN dotnet publish "TemplateDotNet.Api/TemplateDotNet.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TemplateDotNet.Api.dll"]