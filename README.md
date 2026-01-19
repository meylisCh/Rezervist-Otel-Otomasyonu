# Rezervist - Otel Yönetim Sistemi

## Proje Amacı
Rezervist, küçük ve orta ölçekli butik otellerin ve pansiyonların; rezervasyon süreçlerini, oda durumlarını, misafir takiplerini ve kasa işlemlerini dijital ortamda hatasız ve hızlı bir şekilde yönetmelerini sağlayan web tabanlı bir otomasyon sistemidir.

## Hedef Kullanıcı Kitlesi
* **Otel Ön Büro Personelleri (Resepsiyonist):** Günlük giriş-çıkış ve misafir kayıt işlemleri için.
* **Otel Yöneticileri:** Doluluk oranlarını ve kasa durumunu takip etmek için.
* **Pansiyon İşletmecileri:** Manuel defter takibi yerine dijital kayıt tutmak için.

## Senaryo / Kullanım Amacı
Proje, gerçek bir otel işletmesindeki iş akışını simüle eder:
1.  **Rezervasyon:** Müşteri telefonla aradığında resepsiyonist uygun oda sorgular ve rezervasyon oluşturur.
2.  **Check-In (Giriş):** Misafir otele geldiğinde kimlik bilgileri (1774 sayılı kanuna uygun olarak) sisteme girilir ve statü "Giriş Yapıldı" olur.
3.  **Konaklama & Harcama:** Misafir konaklama süresince oda servisi veya ekstra hizmet (minibar, yemek vb.) alırsa, bu harcamalar odaya eklenir.
4.  **Check-Out (Çıkış) & Ödeme:** Çıkış sırasında sistem; oda ücretini ve ekstra harcamaları toplayarak faturayı çıkarır. Ödeme alındıktan sonra oda boşaltılır ve temizlik listesine aktarılır.

## Kullanılan Teknolojiler
Bu proje **MVC (Model-View-Controller)** mimarisine uygun olarak geliştirilmiştir.

* **Programlama Dili:** C#
* **Framework:** ASP.NET Core MVC (8.0)
* **Veritabanı:** PostgreSQL (Entity Framework Core / Code First Yaklaşımı)
* **Arayüz (Frontend):** HTML5, CSS3, Bootstrap 5, JavaScript
* **Geliştirme Ortamı:** Visual Studio Code (MacOS)

## Proje Özellikleri (CRUD İşlemleri)
* **Create (Ekleme):** Yeni rezervasyon oluşturma, yeni misafir kaydı, odaya harcama ekleme.
* **Read (Okuma):** Oda doluluk durumu görüntüleme, konaklayanlar listesi, geçmiş rezervasyonlar, fatura detayları.
* **Update (Güncelleme):** Check-In işlemi (misafir TC güncelleme), Check-Out işlemi, ödeme durumu güncelleme, oda temizlik durumu değiştirme.
* **Delete (Silme):** Hatalı rezervasyonları iptal etme veya silme.

## Tanıtım Videosu
Projenin kullanımını, MVC yapısını ve kodlarını anlattığım tanıtım videosuna aşağıdaki linkten ulaşabilirsiniz:

**[VİDEO LİNKİNİ BURAYA YAPIŞTIRACAKSIN]**

---
*Geliştirici: Meylis Ch*
