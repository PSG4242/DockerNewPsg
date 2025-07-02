# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["DockerNewPsg.csproj", "./"]
RUN dotnet restore "DockerNewPsg.csproj"

# Copy the full source code and build
COPY . .
RUN dotnet build "DockerNewPsg.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "DockerNewPsg.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose default ASP.NET Core port
EXPOSE 8080

ENTRYPOINT ["dotnet", "DockerNewPsg.dll"]
