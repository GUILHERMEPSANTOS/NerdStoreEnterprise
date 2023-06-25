FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Web/MVC/NSE.WebApp.MVC/*.csproj","Web/MVC/NSE.WebApp.MVC/"]
COPY ["./Building Blocks/Core/*.csproj","Building Blocks/Core/"]
COPY ["./Building Blocks/Services/NSE.WebApi.Core/*.csproj","Building Blocks/Services/NSE.WebApi.Core/"]

RUN dotnet restore "Web/MVC/NSE.WebApp.MVC/NSE.WebApp.MVC.csproj"
COPY . .
WORKDIR "Web/MVC/NSE.WebApp.MVC"
RUN dotnet build "NSE.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet build "NSE.WebApp.MVC.csproj" -c Release -o /app/published

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/published .
CMD ["dotnet", "NSE.WebApp.MVC.dll"]