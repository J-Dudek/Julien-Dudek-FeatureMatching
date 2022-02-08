FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Julien.Dudek.FeatureMatching.WebApi/Julien.Dudek.FeatureMatching.WebApi.csproj", "Julien.Dudek.FeatureMatching.WebApi/"]
RUN dotnet restore "src/Julien.Dudek.FeatureMatching.WebApi/Julien.Dudek.FeatureMatching.WebApi.csproj"
COPY . .
WORKDIR "/src/Julien.Dudek.FeatureMatching.WebApi"
RUN dotnet build "Julien.Dudek.FeatureMatching.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Julien.Dudek.FeatureMatching.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Julien.Dudek.FeatureMatching.WebApi.dll"]
