FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Services/Catalogo/NSE.Catalogo.API/*.csproj", "Services/Catalogo/NSE.Catalogo.API/"]
COPY ["./Building Blocks/Core/*.csproj", "Building Blocks/Core/"]
COPY ["./Building Blocks/Services/NSE.WebApi.Core/*.csproj", "Building Blocks/Services/NSE.WebApi.Core/"]
COPY ["./Building Blocks/MessageBus/NSE.MessageBus/*.csproj", "Building Blocks/MessageBus/NSE.MessageBus/"]

RUN dotnet restore "Services/Catalogo/NSE.Catalogo.API/NSE.Catalogo.API.csproj"
COPY . .
WORKDIR "Services/Catalogo/NSE.Catalogo.API"
RUN dotnet build "NSE.Catalogo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Catalogo.API.csproj" -c Release -o /app/published

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/published .
CMD ["dotnet", "NSE.Catalogo.API.dll"]