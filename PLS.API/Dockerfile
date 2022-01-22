FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PLS.API/PLS.API.csproj", "PLS.API/"]
RUN dotnet restore "PLS.API/PLS.API.csproj"
COPY . .
WORKDIR "/src/PLS.API"
RUN dotnet build "PLS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PLS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PLS.API.dll"]
