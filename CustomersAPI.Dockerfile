FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base

LABEL Author="Lukasz Wojtkowski <l.wojtkowski@outlook.com>"
LABEL Version="1.0"

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Customers API/Customers API.csproj", "Customers API/"]
RUN dotnet restore "Customers API/Customers API.csproj"
COPY . .
WORKDIR "/src/Customers API"
RUN dotnet build "Customers API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Customers API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Customers API.dll"]