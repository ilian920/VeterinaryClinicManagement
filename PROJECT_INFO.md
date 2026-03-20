# Veterinary Clinic Management System

## Overview
Complete ASP.NET MVC application for managing a veterinary clinic with appointment booking, animal records, medical history, and vaccinations.

## Architecture
- **VeterinaryClinic.Shared**: Enums (UserRole, AppointmentStatus, AnimalGender)
- **VeterinaryClinic.Data**: EF Core entities, DbContext, repositories
- **VeterinaryClinic.Services**: Business logic services, DTOs, AutoMapper
- **VeterinaryClinicMVC**: ASP.NET MVC UI
- **VeterinaryClinic.Tests**: Unit tests with xUnit

## Technologies
- .NET 10.0
- ASP.NET Core MVC
- Entity Framework Core with MySQL (Pomelo)
- AutoMapper 15.1.1
- Bootstrap 5 (CDN)
- Session-based authentication
- xUnit + Moq for testing

## Database
- **Provider**: MySQL
- **Database Name**: VetClinicDB
- **Connection String**: `Server=localhost;Port=3306;Database=VetClinicDB;User=root;Password=;`

## Seeded Data

### Users
| Username | Password | Role | Email |
|----------|----------|------|-------|
| admin | Admin123! | Admin | admin@vetclinic.bg |
| owner1 | Owner123! | Owner | owner1@mail.bg |
| owner2 | Owner123! | Owner | owner2@mail.bg |

### Veterinarians
- **Иван Петров** - Хирургия (ivan.petrov@vetclinic.bg)
- **Мария Иванова** - Дерматология (maria.ivanova@vetclinic.bg)
- **Георги Димитров** - Ортопедия (georgi.dimitrov@vetclinic.bg)

### Services
- Консултация - 50.00 лв (30 min)
- Ваксинация - 80.00 лв (20 min)
- Хирургия - 300.00 лв (120 min)
- Дентална процедура - 150.00 лв (60 min)
- Грижа за козина - 60.00 лв (45 min)

### Animals (Sample Data)
- **Макс** (Куче/Лабрадор) - Owner: owner1
- **Луна** (Котка/Персийска) - Owner: owner1
- **Рекс** (Куче/Немска овчарка) - Owner: owner2

## Features

### Public Pages
- **Home**: Landing page
- **About**: About the clinic
- **Team**: Display all active veterinarians
- **Services**: List all available services with prices
- **Contact**: Contact information and form
- **Book Appointment**: Public appointment booking (requires login)

### Owner Features
- Register and login
- Manage animals (add, edit, view, delete)
- View animal details with medical history and vaccinations
- Book appointments
- View appointment history
- Cancel appointments

### Admin Features
- Dashboard with statistics
- Manage veterinarians (CRUD)
- Manage services (CRUD)
- Manage appointments (CRUD)
- View all users
- Create appointments for any owner

### Medical Records (Admin/Vet)
- Add medical records for animals
- View medical history
- Track diagnoses, treatments, prescriptions
- Set follow-up dates

### Vaccinations
- Record vaccinations
- Track vaccination dates and next due dates
- Batch numbers for traceability

## Project Structure

```
VeterinaryClinicManagement/
├── VeterinaryClinic.Shared/
│   └── Enums/
│       ├── UserRole.cs
│       ├── AppointmentStatus.cs
│       └── AnimalGender.cs
├── VeterinaryClinic.Data/
│   ├── Entities/
│   │   ├── User.cs
│   │   ├── Veterinarian.cs
│   │   ├── Animal.cs
│   │   ├── Appointment.cs
│   │   ├── VetService.cs
│   │   ├── MedicalRecord.cs
│   │   └── Vaccination.cs
│   ├── Repositories/
│   │   ├── IRepository.cs + Repository.cs
│   │   └── [Specific repositories for each entity]
│   ├── VetClinicDbContext.cs
│   └── DependencyInjection.cs
├── VeterinaryClinic.Services/
│   ├── DTOs/
│   │   ├── UserDto.cs
│   │   ├── VeterinarianDto.cs
│   │   ├── AnimalDto.cs
│   │   ├── AppointmentDto.cs
│   │   ├── VetServiceDto.cs
│   │   ├── MedicalRecordDto.cs
│   │   ├── VaccinationDto.cs
│   │   ├── RegisterUserDto.cs
│   │   └── DashboardStatsDto.cs
│   ├── Interfaces/
│   │   └── [Service interfaces]
│   ├── Implementations/
│   │   └── [Service implementations]
│   ├── Helpers/
│   │   └── PasswordHelper.cs (SHA256 hashing)
│   ├── MappingProfile.cs
│   └── DependencyInjection.cs
├── VeterinaryClinicMVC/
│   ├── Controllers/
│   │   ├── BaseController.cs
│   │   ├── HomeController.cs
│   │   ├── AccountController.cs
│   │   ├── AdminController.cs
│   │   ├── AnimalsController.cs
│   │   ├── AppointmentsController.cs
│   │   ├── MedicalRecordsController.cs
│   │   └── VaccinationsController.cs
│   ├── Data/
│   │   └── DbSeeder.cs
│   ├── Views/
│   │   └── [Razor views - to be created]
│   ├── Program.cs
│   └── appsettings.json
└── VeterinaryClinic.Tests/
    └── Services/
        ├── UserServiceTests.cs
        └── AppointmentServiceTests.cs
```

## How to Run

1. **Prerequisites**:
   - .NET 10.0 SDK installed
   - MySQL server running on localhost:3306
   - Root MySQL user with no password (or update connection string)

2. **Database Setup**:
   ```bash
   # The database will be auto-created on first run
   # Seeding happens automatically in Program.cs
   ```

3. **Build**:
   ```bash
   cd /path/to/VeterinaryClinicManagement
   dotnet build
   ```

4. **Run**:
   ```bash
   cd VeterinaryClinicMVC
   dotnet run
   ```

5. **Access**:
   - Navigate to `https://localhost:5001` or `http://localhost:5000`
   - Login with admin credentials or register as a new owner

6. **Run Tests**:
   ```bash
   cd VeterinaryClinic.Tests
   dotnet test
   ```

## Security Features
- SHA256 password hashing
- Session-based authentication
- Role-based authorization (Admin, Veterinarian, Owner)
- Input validation
- SQL injection protection via EF Core parameterized queries

## Future Enhancements
- Email notifications for appointments
- Online payment integration
- Veterinarian schedules and availability
- Appointment reminders
- File upload for medical documents
- Reporting and analytics
- Multi-language support

## Development Notes
- Uses Repository pattern for data access
- Service layer for business logic
- AutoMapper for entity-DTO mapping
- Session stored in-memory (consider Redis for production)
- Bootstrap 5 for responsive design
- No client-side JavaScript framework (vanilla JS only)

## License
Educational/Demo project - Free to use and modify

## Support
For issues or questions, contact the development team.
