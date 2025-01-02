# 1. Aşama: Build aşaması için resmi .NET SDK imajını kullan
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Çözüm ve proje dosyalarını kopyala
COPY ["classroom_management_backend.sln", "./"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Service.Contracts/Service.Contracts.csproj", "Service.Contracts/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["LoggerService/LoggerService.csproj", "LoggerService/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["ClassroomManagementPresentation/ClassroomManagementPresentation.csproj", "ClassroomManagementPresentation/"]
COPY ["ClassroomManagement/WebAPI/ClassroomManagement.csproj", "ClassroomManagement/"]

# Bağımlılıkları geri yükle
RUN dotnet restore "classroom_management_backend.sln"

# Tüm dosyaları kopyala ve yayınla
COPY . .
WORKDIR "/src/ClassroomManagement"
RUN dotnet publish "ClassroomManagement.csproj" -c Release -o /app/publish

# 2. Aşama: Çalıştırma imajı için resmi .NET Runtime imajını kullan
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Uygulamayı çalıştır
ENTRYPOINT ["dotnet", "ClassroomManagement.dll"]
