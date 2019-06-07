FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Basket API/Basket API.csproj", "Basket API/"]
RUN dotnet restore "Basket API/Basket API.csproj"
COPY . .
WORKDIR "/src/Basket API"
RUN dotnet build "Basket API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Basket API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Basket API.dll"]