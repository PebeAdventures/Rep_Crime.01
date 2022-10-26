#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rep_Crime.01_Crime.API/Rep_Crime.01_Crime.API.csproj", "Rep_Crime.01_Crime.API/"]
COPY ["Commons/Commons.csproj", "Commons/"]
RUN dotnet restore "Rep_Crime.01_Crime.API/Rep_Crime.01_Crime.API.csproj"
COPY . .
WORKDIR "/src/Rep_Crime.01_Crime.API"
RUN dotnet build "Rep_Crime.01_Crime.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rep_Crime.01_Crime.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rep_Crime.01_Crime.API.dll"]