FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./api-gateways/Compras/NSE.Bff.Compras/*.csproj", "api-gateways/Compras/NSE.Bff.Compras/"]
COPY ["./Building Blocks/Core/*.csproj", "Building Blocks/Core/"]
COPY ["./Building Blocks/Services/NSE.WebApi.Core/*.csproj", "Building Blocks/Services/NSE.WebApi.Core/"]
COPY ["./Services/Carrinho/NSE.Carrinho.Api/Protos/*.proto", "api-gateways/Compras/NSE.Bff.Compras/Protos/"]



RUN dotnet restore "api-gateways/Compras/NSE.Bff.Compras/NSE.Bff.Compras.csproj"
COPY . .
WORKDIR "api-gateways/Compras/NSE.Bff.Compras/"
RUN dotnet build "NSE.Bff.Compras.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Bff.Compras.csproj" -c Release -o /app/publish

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "NSE.Bff.Compras.dll"]