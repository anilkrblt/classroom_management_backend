<h1 align="center" id="title">Classroom Management Backend</h1>

<p align="center">
  <img src="https://socialify.git.ci/anilkrblt/classroom_management_backend/image?font=Inter&forks=1&language=1&name=1&owner=1&stargazers=1&theme=Light">
</p>

---

## 📝 Proje Hakkında

Bu proje, üniversite veya okul düzeyinde sınıf, ders ve sınav yönetimini kolaylaştırmak amacıyla geliştirilmiş bir mini OBS sisteminin backend altyapısını sunar.  
Öğrenciler ders programlarını ve sınavlarını görüntüleyebilirken; yöneticiler sınıf bilgilerini, şikayetleri ve sınav çizelgelerini yönetebilir.

## 🚀 Özellikler

- 👤 Rol bazlı kullanıcı yönetimi (öğrenci, öğretmen, admin)
- 🧾 Ders ve sınıf takibi
- 🗓️ Sınav programı oluşturma
- 📋 Şikayet ve hata bildirimi sistemi
- 🔐 JWT tabanlı kimlik doğrulama
- 📑 Swagger destekli API dökümantasyonu

## 🛠️ Kullanılan Teknolojiler

- ASP.NET Core Web API (.NET 6+)
- Entity Framework Core
- MySQL
- JWT Authentication
- Clean Architecture
- Layered Architecture
- Swagger

## 📂 Proje Yapısı
classroom_management_backend/  
├── Contracts/  
├── Entities/  
├── ClassroomManagement/  
├── ClassroomManagement.Tests/  
├── ClassroomManagementPresentation/  
├── GeneticAlgorithm/   
├── LoggerService/  
├── Repository/  
├── Service/  
├── Service.Contracts/   
├── Shared/   


## 🧪 API Dokümantasyonu

Swagger arayüzü üzerinden API uç noktalarını test edebilirsiniz:  
> `https://classroom-management-backend-jcir.onrender.com/swagger/index.html`

## ⚙️ Kurulum

```bash
git clone https://github.com/anilkrblt/classroom_management_backend.git
cd classroom_management_backend
dotnet restore
dotnet run
