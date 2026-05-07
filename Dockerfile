# 1. Etap budowania
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiujemy plik projektu z podfolderu do kontenera
COPY ["ZooManagmentSystem/ZooManagmentSystem.csproj", "ZooManagmentSystem/"]
RUN dotnet restore "ZooManagmentSystem/ZooManagmentSystem.csproj"

# Kopiujemy całą resztę plików z podfolderu
COPY ./ZooManagmentSystem ./ZooManagmentSystem
WORKDIR "/src/ZooManagmentSystem"

# Publikujemy aplikację
RUN dotnet publish "ZooManagmentSystem.csproj" -c Release -o /app/publish

# 2. Etap uruchamiania
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# TUTAJ WPISUJEMY NASZĄ NAZWĘ:
ENTRYPOINT ["dotnet", "ZooManagmentSystem.dll"]