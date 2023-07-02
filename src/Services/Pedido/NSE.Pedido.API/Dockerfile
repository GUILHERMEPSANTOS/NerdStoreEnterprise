FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Services/Pedido/NSE.Pedido.API/*.csproj", "Services/Pedido/NSE.Pedido.API/"]
COPY ["./Services/Pedido/NSE.Pedido.Infra/*.csproj", "Services/Pedido/NSE.Pedido.Infra/"]
COPY ["./Services/Pedido/NSE.Pedido.Domain/*.csproj", "Services/Pedido/NSE.Pedido.Domain/"]
COPY ["./Building Blocks/Services/NSE.WebApi.Core/*.csproj", "Building Blocks/Services/NSE.WebApi.Core/"]
COPY ["./Building Blocks/Core/*.csproj", "Building Blocks/Core/"]
COPY ["./Building Blocks/MessageBus/NSE.MessageBus/*.csproj", "Building Blocks/MessageBus/NSE.MessageBus/"] 

COPY . .
WORKDIR "Services/Pedido/NSE.Pedido.API/"
RUN dotnet restore "NSE.Pedido.API.csproj"
RUN dotnet build "NSE.Pedido.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Pedido.API.csproj" -c Release -o /app/publish

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "NSE.Pedido.API.dll"]

