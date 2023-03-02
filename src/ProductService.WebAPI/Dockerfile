FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ProductService.WebAPI/ProductService.WebAPI.csproj", "src/ProductService.WebAPI/"]
RUN dotnet restore "src/ProductService.WebAPI/ProductService.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/ProductService.WebAPI"
RUN dotnet build "ProductService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductService.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.WebAPI.dll"]
