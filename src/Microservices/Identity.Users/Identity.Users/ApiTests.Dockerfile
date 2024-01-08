#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_HTTPS_PORT=8081
ENV ASPNETCORE_HTTP_PORT=8080
ENV ASPNETCORE_URLS=http://*:8080;https://*:8081

COPY /dev-certificates/*.crt /usr/local/share/ca-certificates/
COPY /dev-certificates/*.pfx /usr/local/share/ca-certificates/
RUN update-ca-certificates

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Identity.Users.Web/Identity.Users.Web.csproj", "Identity.Users.Web/"]
RUN dotnet restore "./Identity.Users.Web/./Identity.Users.Web.csproj"
COPY . .
WORKDIR "/src/Identity.Users.Web"
RUN dotnet build "./Identity.Users.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.Users.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Users.Web.dll"]