# 🐾 ВетКлиника — Система за Управление на Ветеринарна Клиника

> **⚠️ За да видиш целия код в репото си:**
> 1. Отиди в GitHub репото → раздел **"Pull requests"**
> 2. Отвори **PR #1** ("Veterinary Clinic Management System...")
> 3. Натисни **"Ready for review"** (горе вдясно, ако е сиво/Draft)
> 4. Натисни зеления бутон **"Merge pull request"** → **"Confirm merge"**
> 5. След сливането ще видиш **всички файлове в `main`** ✅
>
> 📷 [Виж как изглежда приложението →](#как-изглежда-приложението)

---

A complete ASP.NET Core MVC application for managing veterinary clinic operations including appointments, animal records, medical history, and vaccinations.

## ✨ Features

- **User Management**: Role-based access (Admin, Owner)
- **Animal Records**: Complete pet profiles with medical history
- **Appointment System**: Book and manage appointments
- **Medical Records**: Track diagnoses, treatments, and prescriptions
- **Vaccination Tracking**: Record and monitor vaccination schedules
- **Veterinarian Management**: Admin panel for vet profiles
- **Service Management**: Define and price veterinary services
- **Dashboard**: Statistics and overview for administrators

## 🛠️ Technologies

- **Framework**: ASP.NET Core 10.0 MVC
- **Database**: MySQL with Entity Framework Core (Pomelo)
- **ORM**: Entity Framework Core 10.0
- **Mapping**: AutoMapper 15.1.1
- **UI**: Bootstrap 5 (CDN), Razor Views
- **Authentication**: Session-based authentication
- **Testing**: xUnit with Moq
- **Architecture**: Repository pattern, Service layer

## 🚀 Бързо стартиране (Quick Start)

### Необходимо (Prerequisites)

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) — изтегли и инсталирай
- Git

> **Не е нужен MySQL за локален тест!** Приложението автоматично използва SQLite в Development режим.

### Стъпки

1. **Клонирай репото**
   ```bash
   git clone https://github.com/ilian920/VeterinaryClinicManagement.git
   cd VeterinaryClinicManagement
   ```

2. **Стартирай приложението**
   ```bash
   cd VeterinaryClinicMVC
   dotnet run
   ```

3. **Отвори в браузъра**
   ```
   http://localhost:5222
   ```
   Базата данни (SQLite) се създава и попълва **автоматично** при първо стартиране.

### Тестови акаунти

| Роля | Потребител | Парола |
|------|-----------|--------|
| Администратор | `admin` | `Admin123!` |
| Собственик 1 | `owner1` | `Owner123!` |
| Собственик 2 | `owner2` | `Owner123!` |

### За продукция с MySQL

Редактирай `VeterinaryClinicMVC/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=VetClinicDB;User=root;Password=;",
    "Provider": "MySQL"
  }
}
```

Или използвай Docker:
```bash
docker compose up
# Приложението е на http://localhost:8080
```

## 📸 Как изглежда приложението

### Начална страница
![Начална страница](https://github.com/user-attachments/assets/53d5fe2a-70f6-4eba-8f13-a7a66d93ab79)

### Админ панел
![Админ панел](https://github.com/user-attachments/assets/c657b5ce-f778-4f26-a151-69e0ca644ad5)

## 📁 Структура на проекта

```
VeterinaryClinicManagement/
│
├── VeterinaryClinic.Shared/          # Споделени enums и константи
│   └── Enums/
│
├── VeterinaryClinic.Data/            # Слой за достъп до данни
│   ├── Entities/                     # Домейн модели
│   ├── Repositories/                 # Repository pattern
│   └── VetClinicDbContext.cs         # EF Core DbContext
│
├── VeterinaryClinic.Services/        # Бизнес логика
│   ├── DTOs/                         # Data Transfer Objects
│   ├── Interfaces/                   # Интерфейси на услугите
│   ├── Implementations/              # Имплементации
│   ├── Helpers/                      # Помощни класове
│   └── MappingProfile.cs             # AutoMapper конфигурация
│
├── VeterinaryClinicMVC/              # Presentation слой
│   ├── Controllers/                  # MVC Controllers
│   ├── Views/                        # Razor изгледи
│   ├── Data/                         # Database seeder
│   └── wwwroot/                      # Статични файлове
│
├── VeterinaryClinic.Tests/           # Unit тестове (23 теста)
├── docker-compose.yml                # Docker за MySQL в продукция
└── Dockerfile
```

## 📊 База данни — 7 обекта

- **Users** (Администратори, Собственици)
- **Veterinarians** (Ветеринарни лекари)
- **Animals** (Домашни любимци)
- **Appointments** (Часове)
- **VetServices** (Ветеринарни услуги)
- **MedicalRecords** (Медицински досиета)
- **Vaccinations** (Ваксинации)

## 🔒 Сигурност

- PBKDF2 хеширане на пароли (100 000 итерации, SHA-256)
- Session-based автентикация
- Role-based авторизация
- CSRF защита (`[ValidateAntiForgeryToken]` на всички POST)
- SQL injection защита чрез EF Core
- XSS защита чрез Razor encoding

## 🧪 Тестове

```bash
dotnet test VeterinaryClinic.Tests/
# 23 unit теста — всички минават
```

## 📄 Лиценз

Проектът е предоставен за образователни цели (дипломна работа).

---

**Изграден с ❤️ за ветеринарни клиники — Дипломен проект, 12-ти клас**
