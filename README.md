# 🧾 Employee Payroll API

**Employee Payroll API**, modern bir **.NET 8** tabanlı backend projesidir.  
Proje, **Dapper** altyapısı kullanılarak geliştirilmiş olup **Stored Procedure**, **FluentValidation**, **Swagger**,  
**Unit & Integration Testleri**, ve **Docker Compose** desteği ile profesyonel bir yapıya sahiptir.

> Developed by **Görkem Tok** as part of a technical assessment project.  
> Live API Documentation → [https://employeepayroll.gorkemtok.com/api/docs/index.html](https://employeepayroll.gorkemtok.com/api/docs/index.html)

---

## 🧩 Mimari

| Katman | Açıklama |
|--------|-----------|
| **Controllers** | API uç noktalarını barındırır. Swagger açıklamaları ve response tipleri ile dokümante edilmiştir. |
| **Data** | Dapper Context ve Repository yapıları burada bulunur. |
| **Validators** | FluentValidation ile model doğrulama kuralları. |
| **Tests** | xUnit, Moq ve FluentAssertions kullanılarak Unit & Integration testleri içerir. |
| **Middlewares** | Exception ve global hata yakalama altyapısı. |
| **Models** | API Request & Response modelleri burada bulunur |
---

## ⚙️ Teknoloji ve Araçlar

| Teknoloji | Kullanım Amacı |
|------------|----------------|
| **.NET 8.0** | API geliştirme platformu |
| **Dapper** | ORM yerine hafif ve performanslı data erişimi |
| **SQL Server 2022** | Veritabanı yönetimi |
| **FluentValidation** | Model doğrulama altyapısı |
| **Swagger / Swashbuckle** | API dokümantasyonu |
| **xUnit + Moq + FluentAssertions** | Unit ve Integration testleri |
| **Docker + Docker Compose** | Konteyner tabanlı çalışma ortamı |
| **Microsoft.Data.SqlClient** | SQL bağlantısı için resmi istemci |

---

## 🧠 Özellikler

✅ Employee CRUD işlemleri  
✅ Fazla mesai (Overtime) ve çalışma günü (WorkEntries) yönetimi  
✅ Maaş hesaplama (Stored Procedure tabanlı)  
✅ FluentValidation ile model kontrolleri  
✅ Exception middleware ile merkezi hata yönetimi  
✅ Swagger dokümantasyonu  
✅ Unit & Integration testleri  
✅ Docker Compose ile containerize deployment  

---

## 🗃️ Veritabanı

Proje ile birlikte iki dosya yer almaktadır:

| Dosya | Açıklama |
|--------|-----------|
| `EmployeePayrollDb_Init.sql` | Veritabanı şeması, tablolar, ilişkiler ve örnek veriler |
| `EmployeePayrollDb_Diagram.pdf` | Veritabanı ilişkilerini görsel olarak gösterir |

> Script dosyası çalıştırıldığında veritabanı otomatik olarak oluşturulur.

---

## 📊 Veritabanı Şeması ve Tablolar
Proje, çalışan yönetimi ve maaş hesaplama süreçlerini yönetmek üzere tasarlanmış bir SQL Server veritabanı kullanır.
Aşağıda temel tablolar ve işlevleri özetlenmiştir:
| Tablo Adı               | Açıklama                                                                                        |
| ----------------------- | ----------------------------------------------------------------------------------------------- |
| **Employees**           | Çalışan bilgilerini tutar. (Ad, Soyad, T.C. No, Maaş tipi, Oluşturulma tarihi vb.)              |
| **EmployeeTypes**       | Çalışan türlerini belirtir. (Sabit maaşlı, Günlük ücretli, Fazla mesai dahil vb.)               |
| **WorkEntries**         | Çalışanların günlük çalışma kayıtlarını içerir. (Çalışma tarihi, çalışan ID’si)                 |
| **OvertimeEntries**     | Fazla mesai girişlerini tutar. (Çalışan ID’si, tarih, saat bilgisi)                             |

---

## ⚙️ Stored Procedure Listesi ve Açıklamaları
| Stored Procedure Adı       | Açıklama                                                                                    |
| -------------------------- | ------------------------------------------------------------------------------------------- |
| **sp_AddWorkEntry**        | Yeni bir çalışma günü kaydı ekler. Aynı çalışanın aynı güne ait kaydı varsa tekrar eklemez. |
| **sp_DeleteWorkEntry**     | Belirtilen `WorkEntryId` değerine göre çalışma günü kaydını siler.                          |
| **sp_WorkEntriesHistory**  | Bir çalışanın belirli bir ay içindeki günlük çalışma geçmişini listeler.                    |
| **sp_WorkEntriesSummary**  | Belirtilen ay ve yıla göre tüm çalışanların toplam çalışma gün sayısını döner.              |
| **sp_AddOvertimeEntry**    | Fazla mesai kaydı ekler. Aynı çalışanın aynı tarih için kaydı varsa yenisini oluşturmaz.    |
| **sp_UpdateOvertimeEntry** | Fazla mesai kaydının tarih veya saat bilgisini günceller.                                   |
| **sp_DeleteOvertimeEntry** | Fazla mesai kaydını siler. Kayıt bulunamazsa hata mesajı döner.                             |
| **sp_OvertimeHistory**     | Belirtilen çalışanın seçilen ay içerisindeki fazla mesai geçmişini döner.                   |
| **sp_OvertimeSummary**     | Belirtilen ay ve yıla göre tüm çalışanların toplam fazla mesai saatlerini raporlar.         |
| **sp_CreateEmployee**      | Yeni bir çalışan oluşturur. T.C. kimlik numarası benzersiz olmalıdır.                       |
| **sp_UpdateEmployee**      | Mevcut bir çalışanın bilgilerini günceller.                                                 |
| **sp_GetEmployeesPaged**   | Sayfalama destekli çalışan listesi döner.                                                   |
| **sp_GetEmployeeDetail**   | Belirtilen `EmployeeId` değerine göre detaylı çalışan bilgilerini döner.                    |
| **sp_CalculatePayroll**    | Çalışanın maaşını hesaplar. Maaş tipi (sabit, günlük, sabit + mesai) dikkate alınır.        |
| **sp_GetPayrollReport**    | Belirtilen yıl ve aya göre tüm çalışanların maaş özet raporunu döner.                       |

---

## 📂 Proje Dosya Yapısı
```
EmployeePayroll/
│
├── docker-compose.yml
│
├── EmployeePayroll.Api/
│   ├── Connected Services/         # Visual Studio bağlantı servisleri
│   ├── Dependencies/               # Proje bağımlılıkları
│   ├── Properties/                 # Derleme ayarları
│   │
│   ├── Controllers/                # API endpoint’lerini içeren controller sınıfları
│   ├── Data/                       # Dapper context, repository’ler ve SQL işlemleri
│   ├── Middlewares/                # Global hata yakalama (exception handling) ve logging
│   ├── Models/                     # Request, Response, DTO ve Entity modelleri
│   ├── Validators/                 # FluentValidation kuralları
│   │
│   ├── appsettings.json            # Konfigürasyon dosyası (connection string, swagger vb.)
│   ├── Dockerfile                  # Docker imajı oluşturmak için yapılandırma dosyası
│   ├── EmployeePayroll.Api.http    # Endpoint test istekleri (örnek HTTP çağrıları)
│   └── Program.cs                  # Uygulama başlangıç noktası ve servis kayıtları (DI)
│
├── EmployeePayroll.Tests/
│   ├── Dependencies/               # Test bağımlılıkları
│   ├── IntegrationTests/           # Gerçek veritabanı üzerinde çalışan entegrasyon testleri
│   └── UnitTests/                  # Mock verilerle çalışan birim testleri (xUnit, Moq, FluentAssertions)
│
└── README.md                       # Proje dokümantasyonu
```
---

## 🚀 Docker ile Çalıştırma

Projeyi **Docker Compose** ile birkaç saniyede MSSQL veritabanıyla birlikte çalıştırabilirsiniz. 
Not: Veritabanını kurduktan sonra `EmployeePayrollDb_Init.sql` script dosyasını çalıştırmanız gerekmektedir. MSSQL veritabanı docker içerisinde 7610 portundan dış dünyaya açılmaktadır. Localinizde localhost,7610 server ismiyle MSSQL veritabanına ulaşabilirsiniz.

```bash
git clone https://github.com/gorkem-tok-dev/employee-payroll.git
cd employee-payroll
docker compose up --build
