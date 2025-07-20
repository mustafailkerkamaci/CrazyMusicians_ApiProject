# Çılgın Müzisyenler API

## Projeye Genel Bakış

Bu proje, kendine özgü özelliklere sahip "Çılgın Müzisyenler" listesini yöneten bir ASP.NET Core Web API'sidir. Temel CRUD (Oluştur, Oku, Güncelle, Sil) operasyonlarını (yumuşak silme işlevselliği dahil) uygulamakta ve API dokümantasyonu ile testi için Swagger UI'dan yararlanmaktadır. Proje, temiz kod yapısına, veri doğrulamasına (validation) ve yaygın API tasarım pratiklerine bağlı kalmaya büyük önem vermektedir.

## Özellikler

* **Müzisyen Yönetimi:**
    * **Tüm Müzisyenleri Getir:** Tüm aktif (yumuşak silinmemiş) müzisyenlerin listesini döndürür. (`GET /api/musicians`)
    * **ID ile Müzisyen Getir:** Benzersiz ID'sine göre belirli bir aktif müzisyeni döndürür. (`GET /api/musicians/{id}`)
    * **Yeni Müzisyen Ekle:** Listeye yeni bir müzisyen oluşturur ve ekler. (`POST /api/musicians`)
    * **Müzisyen Güncelle:** Mevcut bir müzisyenin detaylarını günceller. (`PUT /api/musicians/{id}`)
    * **Müzisyeni Yumuşak Sil:** Bir müzisyeni, verilerini listeden fiziksel olarak silmeden "silinmiş" olarak işaretler. (`DELETE /api/musicians/{id}`)
* **Veri Doğrulama:** Veri bütünlüğünü sağlamak için girdi verileri `System.ComponentModel.DataAnnotations` kullanılarak doğrulanır (örn. zorunlu alanlar).
* **Sorgu Parametresi Kullanımı (`[FromQuery]`):** Sorgu parametrelerini kullanarak müzisyenleri ad, meslek veya eğlenceli özelliğe göre aramayı destekler. (`GET /api/musicians/search?query={aramaTerimi}`)
* **Swagger UI Entegrasyonu:** Kolay keşif için etkileşimli API dokümantasyonu ve test arayüzü mevcuttur.

## Kullanılan Teknolojiler

* **ASP.NET Core 8.0:** Web API Çerçevesi
* **C#:** Programlama Dili
* **Swashbuckle.AspNetCore:** Swagger/OpenAPI üretimi ve Swagger UI için
* **Bellek İçi Liste:** Geçici veri depolama için (gelecekte bir veritabanına genişletilebilir)

###Mevcut uç noktaların bir özeti aşağıdadır:

<img width="821" height="954" alt="image" src="https://github.com/user-attachments/assets/71fd36ce-ae38-4a0f-9751-6e7440aeed69" />

