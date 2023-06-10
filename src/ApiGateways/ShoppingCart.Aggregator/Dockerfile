#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/ShoppingCart.Aggregator/ShoppingCart.Aggregator.csproj", "src/ApiGateways/ShoppingCart.Aggregator/"]
RUN dotnet restore "src/ApiGateways/ShoppingCart.Aggregator/ShoppingCart.Aggregator.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/ShoppingCart.Aggregator"
RUN dotnet build "ShoppingCart.Aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShoppingCart.Aggregator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCart.Aggregator.dll"]