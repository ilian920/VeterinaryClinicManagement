using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Services.Implementations;
using VeterinaryClinic.Services.Interfaces;

namespace VeterinaryClinic.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVeterinarianService, VeterinarianService>();
        services.AddScoped<IAnimalService, AnimalService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IVetServiceService, VetServiceService>();
        services.AddScoped<IMedicalRecordService, MedicalRecordService>();
        services.AddScoped<IVaccinationService, VaccinationService>();

        return services;
    }
}
