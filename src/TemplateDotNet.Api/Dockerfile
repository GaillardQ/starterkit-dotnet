FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5001
ENV TEMPLATE_DB="Host=localhost;Port=5432;Database=starterkit-dotnet;Username=postgres;Password=Fuck666"

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/TemplateDotNet.Api/TemplateDotNet.Api.csproj", "src/TemplateDotNet.Api/"]
RUN dotnet restore "src/TemplateDotNet.Api/TemplateDotNet.Api.csproj"
COPY . .
WORKDIR "/src/src/TemplateDotNet.Api"
RUN dotnet build "TemplateDotNet.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "TemplateDotNet.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TemplateDotNet.Api.dll"]
