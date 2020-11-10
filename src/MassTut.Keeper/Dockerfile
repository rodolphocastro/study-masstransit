#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster AS build
WORKDIR /src
COPY ["src/MassTut.Keeper/MassTut.Keeper.csproj", "src/MassTut.Keeper/"]
RUN dotnet restore "src/MassTut.Keeper/MassTut.Keeper.csproj"
COPY . .
WORKDIR "/src/src/MassTut.Keeper"
RUN dotnet build "MassTut.Keeper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTut.Keeper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTut.Keeper.dll"]