FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Services/Pagamento/NSE.Pagamento.API/*.csproj", "Services/Pagamento/NSE.Pagamento.API/"]
COPY ["./Building Blocks/Services/NSE.WebApi.Core/*.csproj", "Building Blocks/Services/NSE.WebApi.Core/"]
COPY ["./Building Blocks/Core/*.csproj", "/Building Blocks/Core/"]
COPY ["./Building Blocks/MessageBus/NSE.MessageBus/*.csproj", "/Building Blocks/MessageBus/NSE.MessageBus/"]
COPY ["./Services/Pagamento/NSE.Pagamento.NerdsPag/*.csproj", "Services/Pagamento/NSE.Pagamento.NerdsPag/"]

RUN dotnet restore "Services/Pagamento/NSE.Pagamento.API/NSE.Pagamento.API.csproj"
COPY . .
WORKDIR "Services/Pagamento/NSE.Pagamento.API/"
RUN dotnet build "NSE.Pagamento.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Pagamento.API.csproj" -c Release -o /app/publish

FROM runtime AS final 
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "NSE.Pagamento.API.dll"]