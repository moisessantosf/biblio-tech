#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Biblio.WebApi/Biblio.WebApi.csproj", "src/Biblio.WebApi/"]
COPY ["src/Biblio.Application/Biblio.Application.csproj", "src/Biblio.Application/"]
COPY ["src/Biblio.Domain/Biblio.Domain.csproj", "src/Biblio.Domain/"]
COPY ["src/Biblio.Data/Biblio.Infrastructure.Data.csproj", "src/Biblio.Data/"]
COPY ["src/Biblio.IoC/Biblio.Infrastructure.IoC.csproj", "src/Biblio.IoC/"]
COPY ["src/Biblio.Infrastructure.Messaging/Biblio.Infrastructure.Messaging.csproj", "src/Biblio.Infrastructure.Messaging/"]
RUN dotnet restore "src/Biblio.WebApi/Biblio.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Biblio.WebApi"
RUN dotnet build "Biblio.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Biblio.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Biblio.WebApi.dll"]