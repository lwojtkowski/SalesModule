FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base

LABEL Author="Lukasz Wojtkowski <l.wojtkowski@outlook.com>"
LABEL Version="1.0"

WORKDIR /app
EXPOSE 83
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Ordering API/Ordering API.csproj", "Ordering API/"]
RUN dotnet restore "Ordering API/Ordering API.csproj"
COPY . .
WORKDIR "/src/Ordering API"
RUN dotnet build "Ordering API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Ordering API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ordering API.dll"]