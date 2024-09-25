# LinkShare Web API (ASP.NET Core 8.0)

## 📖 Proje Tanıtımı
**LinkShare**; kullanıcıların web linklerini paylaşabileceği, ASP.NET Core 8.0 üzerinde geliştirilen yenilikçi bir web API platformudur. Proje, güvenli kimlik doğrulama, link paylaşımı ve yorum özellikleriyle güçlü bir kullanıcı deneyimi sunmaktadır.

## 🎯 Proje Hedefleri
- **Verimli Paylaşım**: Kullanıcıların linkleri hızlı ve etkili bir şekilde paylaşmasını sağlamak.
- **Güvenlik ve Doğrulama**: JWT tabanlı kimlik doğrulama ile güvenliği en üst düzeye çıkarmak.
- **Performans**: Yüksek performanslı ve ölçeklenebilir bir API sunmak.

## 🔧 Kullanılan Teknolojiler
- **Backend**: ASP.NET Core 8.0, RESTful API
- **Veritabanı**: SQL Server
- **Kimlik Doğrulama**: ASP.NET Identity, JWT Token
- **Dökümantasyon**: Swagger
- **Araçlar**: Postman, Git, Visual Studio

## 🏛️ Mimariler
- **Onion Architecture**: Çekirdek iş mantığını dış katmanlardan soyutlayarak sürdürülebilir bir yapı sağlar.
- **Repository Design Pattern**: Veritabanı işlemlerini soyutlar ve daha temiz bir kod yapısı sunar.
- **Mediator Design Pattern**: API isteklerini daha düzenli ve merkezi bir şekilde yönetmek için MediatR kullanılmaktadır.

## 🚀 Özellikler
- **Kullanıcı Kimlik Doğrulama**: JWT tabanlı güvenli giriş ve kayıt.
- **Link Yönetimi**: Link oluşturma, güncelleme, silme ve görüntüleme işlemleri.
- **Yorum Yönetimi**: Kullanıcılar, paylaşılan linklere yorum ekleyebilir ve yönetebilir.
- **Swagger Dökümantasyonu**: API'yi keşfetmek ve test etmek için gelişmiş Swagger arayüzü.

## 📂 API Endpoint'leri

### Kullanıcı Girişi:
- `POST /api/auth/register`: Yeni kullanıcı kaydı
- `POST /api/auth/login`: Giriş yap ve JWT al

### Link Yönetimi:
- `GET /api/links`: Tüm linkleri listele
- `POST /api/links`: Yeni bir link oluştur
- `PUT /api/links/{id}`: Link güncelle
- `DELETE /api/links/{id}`: Link sil

### Yorum Yönetimi:
- `GET /api/comments`: Tüm yorumları listele
- `POST /api/comments`: Yeni bir yorum ekle

## 📅 Gelecek Geliştirmeler
- Mobil uygulama entegrasyonu
- Yapay zeka tabanlı içerik önerileri
- Yeni gelir modelleri ve premium üyelik özellikleri
Projenin nasıl çalıştığını görmek için aşağıdaki demo videosunu izleyebilirsiniz:

[![Demo Video](https://img.youtube.com/vi/KoCqKC_lOBQ/0.jpg)](https://www.youtube.com/watch?v=64fkeQKqawk)
  
## 🛠️ Kurulum Adımları
Projeyi yerel ortamınızda çalıştırmak için aşağıdaki adımları izleyebilirsiniz:

1. 
   ```bash
   ## 1.Depoyu klonlayın:
   git clone https://github.com/hamzaagunduz/LinkShare-WebApi-ASP.NetCore-8.0.git
   ## 2.Proje dizinine gidin:
   cd LinkShare-WebApi-ASP.NetCore-8.0
   ## 3.Gerekli bağımlılıkları yükleyin:
   dotnet restore
   ## 4.Veritabanı bağlantı ayarlarını appsettings.json dosyasından yapılandırın.
   dotnet ef database update
   ## 5.Projeyi başlatın:
   dotnet run



