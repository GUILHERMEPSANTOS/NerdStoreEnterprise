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

RUN dotnet restore "Services/Carrinho/NSE.Carrinho.Api/NSE.Carrinho.Api.csproj"
