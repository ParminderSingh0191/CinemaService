#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Web.Api/CinemaService.Web.Api.csproj", "src/Web.Api/"]
COPY ["src/Web.Api.Library/CinemaService.Web.Api.Library.csproj", "src/Web.Api.Library/"]
COPY ["src/DataLayer/CinemaService.DataLayer.csproj", "src/DataLayer/"]
RUN dotnet restore "src/Web.Api/CinemaService.Web.Api.csproj"
COPY . .
WORKDIR "/src/src/Web.Api"
RUN dotnet build "CinemaService.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CinemaService.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaService.Web.Api.dll"]