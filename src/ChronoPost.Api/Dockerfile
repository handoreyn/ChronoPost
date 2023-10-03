FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5085

ENV ASPNETCORE_URLS=http://+:5085

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/ChronoPost.Api/ChronoPost.Api.csproj", "src/ChronoPost.Api/"]
RUN dotnet restore "src/ChronoPost.Api/ChronoPost.Api.csproj"
COPY . .
WORKDIR "/src/src/ChronoPost.Api"
RUN dotnet build "ChronoPost.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ChronoPost.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChronoPost.Api.dll"]
