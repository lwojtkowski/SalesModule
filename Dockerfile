FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base

LABEL Author="Lukasz Wojtkowski <l.wojtkowski@outlook.com>"
LABEL Version="1.0"

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Ocelot API Gateway/Ocelot API Gateway.csproj", "Ocelot API Gateway/"]
RUN dotnet restore "Ocelot API Gateway/Ocelot API Gateway.csproj"
COPY . .
WORKDIR "/src/Ocelot API Gateway"
RUN dotnet build "Ocelot API Gateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Ocelot API Gateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ocelot API Gateway.dll"]