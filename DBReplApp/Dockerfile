# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the project files
COPY DBReplApp.csproj ./
COPY ../DbConnectors/DbConnectors.csproj ../DbConnectors/DbConnectors.csproj

# Restore dependencies
RUN dotnet restore DBReplApp.csproj

# Copy all source files
COPY . ./
COPY ../DbConnectors ../DbConnectors

# Publish
RUN dotnet publish DBReplApp.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "DBReplApp.dll"]
