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
COPY ["ClassroomManagement.Tests/ClassroomManagement.Tests.csproj", "ClassroomManagement.Tests/"]
COPY ["ClassroomManagementPresentation/ClassroomManagementPresentation.csproj", "ClassroomManagementPresentation/"]
COPY ["ClassroomManagement/ClassroomManagement.csproj", "ClassroomManagement/"]

# Bağımlılıkları geri yükle
RUN dotnet restore "classroom_management_backend.sln"

# Tüm dosyaları kopyala
COPY . .

# Web API projesinin bulunduğu dizine geçiş yap
WORKDIR "/src/ClassroomManagement"

# SQLite veritabanı dosyasını publish dizinine kopyala
COPY ["ClassroomManagement/ClassroomManagement.db", "./"]

# Yayınlama işlemi
RUN dotnet publish "ClassroomManagement.csproj" -c Release -o /app/publish

# 2. Aşama: Çalıştırma imajı için resmi .NET Runtime imajını kullan
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Publish edilmiş dosyaları kopyala
COPY --from=build /app/publish .

# Portu belirt (Render genellikle 10000 ve üstü portları kullanır, burada 8080 örnek)
EXPOSE 8080

# ASP.NET Core uygulamasının hangi portu dinleyeceğini belirt
ENV ASPNETCORE_URLS=http://+:8080

# Uygulamayı çalıştır
ENTRYPOINT ["dotnet", "ClassroomManagement.dll"]
