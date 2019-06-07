FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["SalesModule/SalesModule.csproj", "SalesModule/"]
RUN dotnet restore "SalesModule/SalesModule.csproj"
COPY . .
WORKDIR "/src/SalesModule"
RUN dotnet build "SalesModule.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SalesModule.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SalesModule.dll"]