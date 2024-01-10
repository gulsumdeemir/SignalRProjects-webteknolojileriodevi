#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SignalRApi/SignalRApi.csproj", "SignalRApi/"]
COPY ["SignalR.BusinessLayer/SignalR.BusinessLayer.csproj", "SignalR.BusinessLayer/"]
COPY ["SignalR.DataAccessLayer/SignalR.DataAccessLayer.csproj", "SignalR.DataAccessLayer/"]
COPY ["SignalR.EntityLayer/SignalR.EntityLayer.csproj", "SignalR.EntityLayer/"]
COPY ["SignalR.DtoLayer/SignalR.DtoLayer.csproj", "SignalR.DtoLayer/"]
RUN dotnet restore "./SignalRApi/./SignalRApi.csproj"
COPY . .
WORKDIR "/src/SignalRApi"
RUN dotnet build "./SignalRApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SignalRApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SignalRApi.dll"]