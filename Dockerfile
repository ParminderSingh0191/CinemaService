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

# Run unit tests
WORKDIR /src
RUN mkdir -p /src/tests/results

ARG FAIL_ON_UNIT_TESTS_FAILURE=1

RUN dotnet test CinemaService.sln -r /src/tests/results --logger:trx /p:CollectCoverage=true; \
    rc=$?; \
    if [ $FAIL_ON_UNIT_TESTS_FAILURE = 1 ]; then exit $rc; \
    elif [ $rc = 0 ]; then touch /src/tests/results/tests_succeeded; \
    else touch /src/tests/results/tests_failed; \
    fi

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaService.Web.Api.dll"]