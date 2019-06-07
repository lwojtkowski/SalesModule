FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Authentication API/Authentication API.csproj", "Authentication API/"]
RUN dotnet restore "Authentication API/Authentication API.csproj"
COPY . .
WORKDIR "/src/Authentication API"
RUN dotnet build "Authentication API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Authentication API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Authentication API.dll"]