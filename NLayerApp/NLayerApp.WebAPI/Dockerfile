#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NLayerApp.WebAPI/NLayerApp.WebAPI.csproj", "NLayerApp.WebAPI/"]
COPY ["NLayerApp.BLL/NLayerApp.BLL.csproj", "NLayerApp.BLL/"]
COPY ["NLayerApp.DAL/NLayerApp.DAL.csproj", "NLayerApp.DAL/"]
RUN dotnet restore "NLayerApp.WebAPI/NLayerApp.WebAPI.csproj"
COPY . .
WORKDIR "/src/NLayerApp.WebAPI"
RUN dotnet build "NLayerApp.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NLayerApp.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NLayerApp.WebAPI.dll"]
