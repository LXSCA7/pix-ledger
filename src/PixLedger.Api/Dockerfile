FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/PixLedger.Api/PixLedger.Api.csproj", "src/PixLedger.Api/"]
COPY ["src/PixLedger.Domain/PixLedger.Domain.csproj", "src/PixLedger.Domain/"]
COPY ["src/PixLedger.Infrastructure/PixLedger.Infrastructure.csproj", "src/PixLedger.Infrastructure/"]
COPY ["src/PixLedger.Application/PixLedger.Application.csproj", "src/PixLedger.Application/"]

RUN dotnet restore "src/PixLedger.Api/PixLedger.Api.csproj"

COPY . .

WORKDIR "/src/src/PixLedger.Api"
RUN dotnet build "PixLedger.Api.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "PixLedger.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

USER app

COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet", "PixLedger.Api.dll" ]
