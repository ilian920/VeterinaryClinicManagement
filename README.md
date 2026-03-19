# 🐾 Veterinary Clinic Management System

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

## 🚀 Quick Start

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- MySQL Server (localhost:3306)
- Git

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd VeterinaryClinicManagement
   ```

2. **Update database connection** (if needed)
   Edit `VeterinaryClinicMVC/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=3306;Database=VetClinicDB;User=root;Password=;"
     }
   }
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   cd VeterinaryClinicMVC
   dotnet run
   ```

5. **Access the application**
   - Open browser and navigate to: `https://localhost:5001`
   - The database will be created and seeded automatically on first run

### Default Credentials

**Admin Account:**
- Username: `admin`
- Password: `Admin123!`

**Owner Accounts:**
- Username: `owner1` | Password: `Owner123!`
- Username: `owner2` | Password: `Owner123!`

## 📁 Project Structure

```
VeterinaryClinicManagement/
│
├── VeterinaryClinic.Shared/          # Shared enums and constants
│   └── Enums/
│
├── VeterinaryClinic.Data/            # Data access layer
│   ├── Entities/                     # Domain models
│   ├── Repositories/                 # Repository pattern implementation
│   └── VetClinicDbContext.cs         # EF Core DbContext
│
├── VeterinaryClinic.Services/        # Business logic layer
│   ├── DTOs/                         # Data Transfer Objects
│   ├── Interfaces/                   # Service contracts
│   ├── Implementations/              # Service implementations
│   ├── Helpers/                      # Utility classes
│   └── MappingProfile.cs             # AutoMapper configuration
│
├── VeterinaryClinicMVC/              # Presentation layer
│   ├── Controllers/                  # MVC Controllers
│   ├── Views/                        # Razor views
│   ├── Data/                         # Database seeder
│   └── wwwroot/                      # Static files
│
└── VeterinaryClinic.Tests/           # Unit tests
    └── Services/                     # Service layer tests
```

## 🎯 Use Cases

### For Pet Owners
- Register and create account
- Add and manage pet profiles
- Book appointments with veterinarians
- View pet's medical history and vaccination records
- Track upcoming appointments

### For Administrators
- Manage veterinarian profiles
- Define and update service offerings
- View system statistics and analytics
- Manage all appointments
- Access complete system data

## 🧪 Testing

Run the test suite:

```bash
cd VeterinaryClinic.Tests
dotnet test
```

## 📊 Database Schema

The system includes 7 main entities:
- **Users** (Admin, Owners)
- **Veterinarians**
- **Animals** (Pets)
- **Appointments**
- **VetServices**
- **MedicalRecords**
- **Vaccinations**

See `PROJECT_INFO.md` for detailed schema information.

## 🔒 Security

- SHA256 password hashing
- Session-based authentication
- Role-based authorization
- CSRF protection
- SQL injection prevention via EF Core
- XSS protection via Razor encoding

## 📝 Development Notes

- Clean Architecture with separation of concerns
- Repository pattern for data access
- Service layer for business logic
- DTO pattern for data transfer
- AutoMapper for object mapping
- Dependency Injection throughout
- Session management for authentication

## 🤝 Contributing

This is an educational/demo project. Feel free to fork and enhance!

## 📄 License

This project is provided as-is for educational purposes.

## 📞 Support

For detailed information, see `PROJECT_INFO.md` in the root directory.

---

**Built with ❤️ for veterinary clinics**

ASP.NET MVC Veterinary Clinic Management System - Diploma Project
