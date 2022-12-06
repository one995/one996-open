#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Like.LikeYou/Like.LikeYou.csproj", "Like.LikeYou/"]
COPY ["Like.BLL/Like.BLL.csproj", "Like.BLL/"]
COPY ["Like.Dal/Like.Dao.csproj", "Like.Dal/"]
COPY ["Like.Common/Like.Common.csproj", "Like.Common/"]
COPY ["Like.Model/Like.Model.csproj", "Like.Model/"]
COPY ["Like.Expand/Like.Expand.csproj", "Like.Expand/"]
RUN dotnet restore "Like.LikeYou/Like.LikeYou.csproj"
COPY . .
WORKDIR "/src/Like.LikeYou"
RUN dotnet build "Like.LikeYou.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Like.LikeYou.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Like.LikeYou.dll"]