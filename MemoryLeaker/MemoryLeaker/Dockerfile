FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MemoryLeaker/MemoryLeaker.csproj", "MemoryLeaker/"]
RUN dotnet restore "MemoryLeaker/MemoryLeaker.csproj"
COPY . .
WORKDIR "/src/MemoryLeaker"
RUN dotnet build "MemoryLeaker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MemoryLeaker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MemoryLeaker.dll"]
