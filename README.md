<div align="center">

# 🏨 REZERVİST | Otel Yönetim Sistemi

**Modern, Hızlı ve Kullanıcı Dostu Otel Otomasyonu**

[![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-purple?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-Language-239120?style=for-the-badge&logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-336791?style=for-the-badge&logo=postgresql)](https://www.postgresql.org/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?style=for-the-badge&logo=bootstrap)](https://getbootstrap.com/)
[![Status](https://img.shields.io/badge/Status-Tamamlandı-success?style=for-the-badge)]()

[Proje Amacı](#-proje-amacı) • [Teknolojiler](#-kullanılan-teknolojiler) • [Özellikler](#-proje-özellikleri-ve-crud) • [Video](#-tanıtım-videosu)

</div>

---

## 🎯 Proje Amacı
> **"Kağıt kalemi bırakın, dijitale geçin."**

**Rezervist**; küçük ve orta ölçekli butik otellerin, pansiyonların ve konaklama tesislerinin günlük operasyonlarını dijital ortamda yönetmelerini sağlayan web tabanlı bir otomasyon sistemidir.

Bu projenin temel amacı; manuel rezervasyon takibinden kaynaklanan hataları (çifte rezervasyon, kayıp kayıtlar) ortadan kaldırmak, kasa giriş-çıkışlarını şeffaf bir şekilde denetlemek ve misafir memnuniyetini artırmaktır.

## 👥 Hedef Kullanıcı Kitlesi

| Kullanıcı Tipi | Kullanım Amacı |
| :--- | :--- |
| 🛎️ **Resepsiyonist** | Hızlı Check-In/Check-Out, oda sorgulama ve misafir kaydı. |
| 👔 **Otel Müdürü** | Günlük kasa takibi, doluluk oranları ve gelir raporlaması. |
| 🏡 **Pansiyon Sahibi** | Odaların temizlik durumlarını ve anlık doluluğu tek ekrandan yönetme. |

---

## 🎬 Senaryo ve Kullanım Akışı

Sistem, gerçek bir otelcilik senaryosu üzerine kurgulanmıştır ve aşağıdaki döngüyü yönetir:

1.  📞 **Rezervasyon:** Misafir aradığında, resepsiyonist tarih aralığına göre **müsait odaları** filtreler.
2.  📝 **Kayıt (Check-In):** Misafir otele geldiğinde kimlik bilgileri (TCKN) girilir ve oda **"Dolu"** statüsüne geçer.
3.  ☕ **Harcama:** Konaklama süresince yapılan ekstra harcamalar (Oda servisi, minibar) misafirin hesabına tek tıkla işlenir.
4.  💳 **Çıkış (Check-Out):** Sistem, oda ücreti + ekstraları hesaplar. Ödeme alındıktan sonra fatura dökümü verilir ve oda **"Temizlik Bekliyor"** moduna geçer.

---

## 💻 Kullanılan Teknolojiler

Proje, **MVC (Model-View-Controller)** mimari yapısına sadık kalınarak geliştirilmiştir.

| Kategori | Teknoloji | Açıklama |
| :--- | :--- | :--- |
| **Backend** | • C# <br> • ASP.NET Core MVC 8.0 | Uygulamanın sunucu tarafı ve iş mantığı. |
| **Veritabanı** | • PostgreSQL <br> • Entity Framework Core | Verilerin güvenli ve ilişkisel olarak tutulması. |
| **Frontend** | • HTML5 / CSS3 <br> • Bootstrap 5 | Responsive (Mobil Uyumlu) ve modern arayüz tasarımı. |
| **Araçlar** | • Git & GitHub <br> • Visual Studio Code | Versiyon kontrolü ve geliştirme ortamı. |

---

## 🚀 Proje Özellikleri ve CRUD

| İşlem | Özellik | Detay |
| :--- | :--- | :--- |
| **Create** | ➕ Rezervasyon Oluşturma | Misafir ve tarih seçimi ile çakışma kontrollü kayıt. |
| **Read** | 📋 Listeleme | Anlık konaklayanlar, boş odalar ve kasa hareketleri. |
| **Update** | 🔄 Güncelleme | Check-In/Out işlemleri, ödeme alma, temizlik durumu değişimi. |
| **Delete** | ❌ Silme/İptal | Hatalı veya iptal edilen rezervasyonların sistemden kaldırılması. |

---

## 📺 Tanıtım Videosu

Projenin çalışır halini, MVC yapısını ve kodlarını detaylı incelediğim tanıtım videosunu aşağıdan izleyebilirsiniz:

[![YouTube Video İzle](https://img.shields.io/badge/YouTube-Video_İzle-red?style=for-the-badge&logo=youtube)](https://youtu.be/G2ZIwmEenH4)

*(Video Süresi: 08:15 Dakika)*

---

<div align="center">

*Bu proje, Web Tabanlı Programlama dersi final ödevi kapsamında geliştirilmiştir.*
<br>
👨‍💻 **Geliştirici:** MEYLİS CHARYYEV

</div>
