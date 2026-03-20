using VeterinaryClinic.Data;
using VeterinaryClinic.Data.Entities;
using VeterinaryClinic.Services.Helpers;
using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinicMVC.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(VetClinicDbContext context)
    {
        if (context.Users.Any())
        {
            return;
        }

        // Seed Admin User
        var admin = new User
        {
            Username = "admin",
            PasswordHash = PasswordHelper.HashPassword("Admin123!"),
            Email = "admin@vetclinic.bg",
            FirstName = "Admin",
            LastName = "User",
            Phone = "0888123456",
            Role = UserRole.Admin,
            CreatedAt = DateTime.Now,
            IsActive = true
        };
        context.Users.Add(admin);

        // Seed Veterinarians
        var veterinarians = new[]
        {
            new Veterinarian
            {
                FirstName = "Иван",
                LastName = "Петров",
                Specialization = "Хирургия",
                Phone = "0888111222",
                Email = "ivan.petrov@vetclinic.bg",
                Bio = "Специалист по малки животни с 15 години опит в ветеринарната хирургия.",
                ImageUrl = "/images/vet-placeholder.jpg",
                IsActive = true
            },
            new Veterinarian
            {
                FirstName = "Мария",
                LastName = "Иванова",
                Specialization = "Дерматология",
                Phone = "0888222333",
                Email = "maria.ivanova@vetclinic.bg",
                Bio = "Експерт по кожни заболявания при домашни любимци.",
                ImageUrl = "/images/vet-placeholder.jpg",
                IsActive = true
            },
            new Veterinarian
            {
                FirstName = "Георги",
                LastName = "Димитров",
                Specialization = "Ортопедия",
                Phone = "0888333444",
                Email = "georgi.dimitrov@vetclinic.bg",
                Bio = "Специалист по ортопедия и рехабилитация на животни.",
                ImageUrl = "/images/vet-placeholder.jpg",
                IsActive = true
            }
        };
        context.Veterinarians.AddRange(veterinarians);

        // Seed Services
        var services = new[]
        {
            new VetService
            {
                Name = "Консултация",
                Description = "Общ преглед и консултация",
                Price = 50.00m,
                DurationMinutes = 30,
                IsActive = true
            },
            new VetService
            {
                Name = "Ваксинация",
                Description = "Ваксинация срещу основни заболявания",
                Price = 80.00m,
                DurationMinutes = 20,
                IsActive = true
            },
            new VetService
            {
                Name = "Хирургия",
                Description = "Хирургична интервенция",
                Price = 300.00m,
                DurationMinutes = 120,
                IsActive = true
            },
            new VetService
            {
                Name = "Дентална процедура",
                Description = "Почистване на зъби и дентални процедури",
                Price = 150.00m,
                DurationMinutes = 60,
                IsActive = true
            },
            new VetService
            {
                Name = "Грижа за козина",
                Description = "Професионална грижа за козина",
                Price = 60.00m,
                DurationMinutes = 45,
                IsActive = true
            }
        };
        context.VetServices.AddRange(services);

        // Seed Owner Users
        var owners = new[]
        {
            new User
            {
                Username = "owner1",
                PasswordHash = PasswordHelper.HashPassword("Owner123!"),
                Email = "owner1@mail.bg",
                FirstName = "Петър",
                LastName = "Стоянов",
                Phone = "0888555666",
                Role = UserRole.Owner,
                CreatedAt = DateTime.Now,
                IsActive = true
            },
            new User
            {
                Username = "owner2",
                PasswordHash = PasswordHelper.HashPassword("Owner123!"),
                Email = "owner2@mail.bg",
                FirstName = "Елена",
                LastName = "Василева",
                Phone = "0888777888",
                Role = UserRole.Owner,
                CreatedAt = DateTime.Now,
                IsActive = true
            }
        };
        context.Users.AddRange(owners);

        await context.SaveChangesAsync();

        // Seed Animals (need owner IDs)
        var owner1 = context.Users.First(u => u.Username == "owner1");
        var owner2 = context.Users.First(u => u.Username == "owner2");

        var animals = new[]
        {
            new Animal
            {
                Name = "Макс",
                Species = "Куче",
                Breed = "Лабрадор",
                DateOfBirth = new DateTime(2020, 5, 15),
                Gender = AnimalGender.Male,
                Weight = 30.5m,
                Color = "Жълт",
                Notes = "Много приятелски настроен",
                OwnerId = owner1.Id
            },
            new Animal
            {
                Name = "Луна",
                Species = "Котка",
                Breed = "Персийска",
                DateOfBirth = new DateTime(2021, 3, 10),
                Gender = AnimalGender.Female,
                Weight = 4.2m,
                Color = "Бяла",
                Notes = "Нуждае се от специална диета",
                OwnerId = owner1.Id
            },
            new Animal
            {
                Name = "Рекс",
                Species = "Куче",
                Breed = "Немска овчарка",
                DateOfBirth = new DateTime(2019, 8, 22),
                Gender = AnimalGender.Male,
                Weight = 35.0m,
                Color = "Черно и кафяво",
                Notes = "Добре обучен",
                OwnerId = owner2.Id
            }
        };
        context.Animals.AddRange(animals);

        await context.SaveChangesAsync();
    }
}
