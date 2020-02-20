FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Gap.NetFest.CashDeskWebServices/Gap.NetFest.CashDeskWebServices.csproj", "Gap.NetFest.CashDeskWebServices/"]
RUN dotnet restore "Gap.NetFest.CashDeskWebServices/Gap.NetFest.CashDeskWebServices.csproj"
COPY . .
WORKDIR "/src/Gap.NetFest.CashDeskWebServices"
RUN dotnet build "Gap.NetFest.CashDeskWebServices.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Gap.NetFest.CashDeskWebServices.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gap.NetFest.CashDeskWebServices.dll"]