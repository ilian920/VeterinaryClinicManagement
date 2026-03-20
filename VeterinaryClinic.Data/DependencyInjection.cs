using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Data.Repositories;

namespace VeterinaryClinic.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVeterinarianRepository, VeterinarianRepository>();
        services.AddScoped<IAnimalRepository, AnimalRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IVetServiceRepository, VetServiceRepository>();
        services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
        services.AddScoped<IVaccinationRepository, VaccinationRepository>();

        return services;
    }
}
