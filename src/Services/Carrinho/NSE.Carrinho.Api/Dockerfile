FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR src/
COPY ["./Services/Carrinho/NSE.Carrinho.Api/*.csproj", "Services/Carrinho/NSE.Carrinho.Api/"]
COPY ["./Building Blocks/Core/*.csproj", "Building Blocks/Core/"]
COPY ["./Building Blocks/Services/NSE.WebApi.Core/*.csproj", "Building Blocks/Services/NSE.WebApi.Core/"]
COPY ["./Building Blocks/MessageBus/NSE.MessageBus/*.csproj", "Building Blocks/MessageBus/NSE.MessageBus/"]

COPY . .
WORKDIR "Services/Carrinho/NSE.Carrinho.Api/"
RUN dotnet restore "NSE.Carrinho.API.csproj"
RUN dotnet build "NSE.Carrinho.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Carrinho.API.csproj" -c Release -o /app/publish

FROM runtime AS final 
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet","NSE.Carrinho.API.dll"]