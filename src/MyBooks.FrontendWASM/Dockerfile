# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MyBooks.FrontendWASM/MyBooks.FrontendWASM.csproj", "src/MyBooks.FrontendWASM/"]
RUN dotnet restore "./src/MyBooks.FrontendWASM/MyBooks.FrontendWASM.csproj"
COPY . .
WORKDIR "/src/src/MyBooks.FrontendWASM"
RUN dotnet build "./MyBooks.FrontendWASM.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyBooks.FrontendWASM.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final

# Copy the published Blazor WASM files
COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html

# Copy custom nginx configuration
COPY src/MyBooks.FrontendWASM/nginx.conf /etc/nginx/nginx.conf

EXPOSE 80