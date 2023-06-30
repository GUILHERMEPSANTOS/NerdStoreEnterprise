FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Services/Identidade/NSE.Identidade.API/*.csproj", "/Services/Identidade/NSE.Identidade.API/"]
COPY ["./Building Blocks/Services/NSE.WebApi.Core/*.csproj", "/Building Blocks/Services/NSE.WebApi.Core/"]
COPY ["./Building Blocks/Core/*.csproj", "/Building Blocks/Core/"]
COPY ["./Building Blocks/MessageBus/NSE.MessageBus/*.csproj", "/Building Blocks/MessageBus/NSE.MessageBus/"]

RUN dotnet restore "/Services/Identidade/NSE.Identidade.API/NSE.Identidade.API.csproj"
COPY . .
WORKDIR "./Services/Identidade/NSE.Identidade.API/"
RUN dotnet build "NSE.Identidade.API.csproj" -c Release -o /app/build

FROM build AS published
RUN dotnet publish "NSE.Identidade.API.csproj" -c Release -o /app/publish

FROM runtime AS final
WORKDIR /app
COPY --from=published /app/publish/ . 
CMD ["dotnet", "NSE.Identidade.API.dll"]
 