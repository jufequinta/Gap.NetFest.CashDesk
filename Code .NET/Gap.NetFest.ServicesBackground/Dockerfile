FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY  ["Gap.NetFest.AzureStorage/Gap.NetFest.AzureStorage.csproj", "Gap.NetFest.AzureStorage/"]
COPY  ["Gap.NetFest.Core/Gap.NetFest.Core.csproj", "Gap.NetFest.Core/"]
COPY  ["Gap.NetFest.ServicesBackground/Gap.NetFest.ServicesBackground.csproj", "Gap.NetFest.ServicesBackground/"]
RUN dotnet restore "Gap.NetFest.ServicesBackground/Gap.NetFest.ServicesBackground.csproj"
COPY . .
WORKDIR "/src/Gap.NetFest.ServicesBackground"
RUN dotnet build "Gap.NetFest.ServicesBackground.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Gap.NetFest.ServicesBackground.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gap.NetFest.ServicesBackground.dll"]
