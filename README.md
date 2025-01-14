# BudgetApp - Kişisel Finans Yönetim Uygulaması

BudgetApp, günlük finansal işlemlerinizi takip etmenizi, bütçenizi planlamanızı ve finansal hedeflerinize ulaşmanızı sağlayan kapsamlı bir web uygulamasıdır.

## 🌟 Özellikler

### Temel Özellikler
- 💰 Gelir ve gider takibi
- 📊 Detaylı finansal raporlama
- 🏷️ Özelleştirilebilir kategori yönetimi
- 📅 Aylık ve yıllık bütçe planlaması
- 📈 Gerçek zamanlı grafik ve analizler

### Gelişmiş Özellikler
- 🔔 Akıllı bildirim sistemi
- 🎯 Finansal hedef belirleme ve takibi
- 💳 Çoklu hesap yönetimi
- 📱 Mobil uyumlu tasarım
- 🔄 Otomatik yinelenen işlem kaydı

## 🛠️ Teknoloji Stack

### Mobile Application
- React Native
- Expo
- Redux Toolkit (State yönetimi)
- Material-UI (UI framework)
- Chart.js (Grafikler için)
- Axios (HTTP istekleri)

### Backend
- c# core 8 
- Entity Framework Core 8
- MSSQL (Veritabanı)
- JWT (Kimlik doğrulama)
- Socket.io (Gerçek zamanlı bildirimler)

## ⚙️ Kurulum

### Ön Gereksinimler
- npm veya yarn

### Kurulum Adımları

1. Projeyi klonlayın:
2. Bağımlılıkları yükleyin:
3. Çevre değişkenlerini ayarlayın:
4. Uygulamayı başlatın:

## 📱 Kullanım

1. Kayıt olun veya giriş yapın
2. Ana dashboard üzerinden finansal durumunuzu görüntüleyin
3. Yeni işlem ekleyin (gelir/gider)
4. Kategoriler oluşturun ve düzenleyin
5. Raporları inceleyin ve analiz edin
6. Finansal hedefler belirleyin

## 🤝 Katkıda Bulunma

1. Fork'layın
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişikliklerinizi commit edin (`git commit -m 'feat: Amazing feature added'`)
4. Branch'inizi push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

## 📝 Kod Standartları

- ESLint kurallarına uygun geliştirme
- Prettier ile kod formatı
- Conventional Commits standartlarına uygun commit mesajları
- Jest ile birim testleri

## 🔑 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakınız.

## 📞 İletişim

- Website: [budgetapp.com](https://budgetapp.com)
- Email: info@budgetapp.com
- Twitter: [@budgetapp](https://twitter.com/budgetapp)

## 🙏 Teşekkürler

Bu projeye katkıda bulunan herkese teşekkür ederiz!

github adresi: https://github.com/LeventOzcelik89/BudgetApp



src/
├── api/
│   ├── axios.ts
│   ├── auth.ts
│   ├── transactions.ts
│   ├── categories.ts
│   └── budgets.ts
├── components/
│   ├── common/
│   │   ├── Button.tsx
│   │   ├── Input.tsx
│   │   └── Loading.tsx
│   ├── auth/
│   │   ├── LoginForm.tsx
│   │   └── RegisterForm.tsx
│   └── transaction/
│       ├── TransactionList.tsx
│       └── TransactionCard.tsx
├── screens/
│   ├── auth/
│   │   ├── LoginScreen.tsx
│   │   └── RegisterScreen.tsx
│   ├── dashboard/
│   │   └── DashboardScreen.tsx
│   └── transactions/
│       └── TransactionsScreen.tsx
├── store/
│   ├── index.ts
│   ├── authSlice.ts
│   └── transactionSlice.ts
├── navigation/
│   ├── AppNavigator.tsx
│   └── AuthNavigator.tsx
├── hooks/
│   └── useAuth.ts
├── utils/
│   ├── currency.ts
│   └── date.ts
└── types/
    └── index.ts
