FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Services/Cliente/NSE.Cliente.API/*.csproj", "Services/Cliente/NSE.Cliente.API/"]
COPY ["./Building Blocks/Core/*.csproj", "Building Blocks/Core/"]
COPY ["./Building Blocks/Services/NSE.WebApi.Core/*.csproj","Building Blocks/Services/NSE.WebApi.Core/"]
COPY ["./Building Blocks/MessageBus/NSE.MessageBus/*.csproj", "Building Blocks/MessageBus/NSE.MessageBus/"]

RUN dotnet restore "Services/Cliente/NSE.Cliente.API/NSE.Cliente.API.csproj"
COPY . .
WORKDIR "Services/Cliente/NSE.Cliente.API/"
RUN dotnet build "NSE.Cliente.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Cliente.API.csproj" -c Release -o /app/publish

FROM runtime AS final
WORKDIR /app
COPY  --from=publish /app/publish .
CMD ["dotnet", "NSE.Cliente.API.dll"]