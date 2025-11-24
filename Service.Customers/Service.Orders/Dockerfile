# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Service.Orders/Service.Orders.csproj", "Service.Orders/"]
RUN dotnet restore "Service.Orders/Service.Orders.csproj"
COPY . .
WORKDIR /src/Service.Orders
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Service.Orders.dll"]
