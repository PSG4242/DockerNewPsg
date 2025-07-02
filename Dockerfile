# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["DockerNewPsg.csproj", "./"]
RUN dotnet restore "DockerNewPsg.csproj"
COPY . .
RUN dotnet publish "DockerNewPsg.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# ✅ Install CA certificates (fixes TLS/SSL)
RUN apt-get update && apt-get install -y ca-certificates && update-ca-certificates

COPY --from=build /app/publish .

# ✅ Optional but helpful
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "DockerNewPsg.dll"]
