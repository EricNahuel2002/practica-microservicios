# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Service.Customers/Service.Customers.csproj", "Service.Customers/"]
RUN dotnet restore "Service.Customers/Service.Customers.csproj"
COPY . .
WORKDIR /src/Service.Customers
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Service.Customers.dll"]
