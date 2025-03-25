#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DeveloperEvaluation.WebApi/DeveloperEvaluation.WebApi.csproj", "src/DeveloperEvaluation.WebApi/"]
COPY ["src/DeveloperEvaluation.IoC/DeveloperEvaluation.IoC.csproj", "src/DeveloperEvaluation.IoC/"]
COPY ["src/DeveloperEvaluation.Domain/DeveloperEvaluation.Domain.csproj", "src/DeveloperEvaluation.Domain/"]
COPY ["src/DeveloperEvaluation.Common/DeveloperEvaluation.Common.csproj", "src/DeveloperEvaluation.Common/"]
COPY ["src/DeveloperEvaluation.Application/DeveloperEvaluation.Application.csproj", "src/DeveloperEvaluation.Application/"]
COPY ["src/DeveloperEvaluation.ORM/DeveloperEvaluation.ORM.csproj", "src/DeveloperEvaluation.ORM/"]
RUN dotnet restore "./src/DeveloperEvaluation.WebApi/DeveloperEvaluation.WebApi.csproj"
COPY . .
WORKDIR "/src/src/DeveloperEvaluation.WebApi"
RUN dotnet build "./DeveloperEvaluation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DeveloperEvaluation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeveloperEvaluation.WebApi.dll"]