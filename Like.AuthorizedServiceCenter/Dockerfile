#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 6000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Like.AuthorizedServiceCenter/Like.AuthorizedServiceCenter.csproj", "Like.AuthorizedServiceCenter/"]
COPY ["Like.BLL/Like.BLL.csproj", "Like.BLL/"]
COPY ["Like.Dal/Like.Dao.csproj", "Like.Dal/"]
COPY ["Like.Common/Like.Common.csproj", "Like.Common/"]
COPY ["Like.Model/Like.Model.csproj", "Like.Model/"]
RUN dotnet restore "Like.AuthorizedServiceCenter/Like.AuthorizedServiceCenter.csproj"
COPY . .
WORKDIR "/src/Like.AuthorizedServiceCenter"
RUN dotnet build "Like.AuthorizedServiceCenter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Like.AuthorizedServiceCenter.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Like.AuthorizedServiceCenter.dll"]