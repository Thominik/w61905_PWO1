FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["w61905_PWO1/w61905_PWO1.csproj", "w61905_PWO1/"]
RUN dotnet restore "w61905_PWO1/w61905_PWO1.csproj"
COPY . .
WORKDIR "/src/w61905_PWO1"
RUN dotnet build "w61905_PWO1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "w61905_PWO1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "w61905_PWO1.dll"]
