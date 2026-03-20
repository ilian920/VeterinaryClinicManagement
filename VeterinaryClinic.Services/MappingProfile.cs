using AutoMapper;
using VeterinaryClinic.Data.Entities;
using VeterinaryClinic.Services.DTOs;

namespace VeterinaryClinic.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        
        CreateMap<Veterinarian, VeterinarianDto>().ReverseMap();
        
        CreateMap<Animal, AnimalDto>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => $"{src.Owner.FirstName} {src.Owner.LastName}"))
            .ReverseMap()
            .ForMember(dest => dest.Owner, opt => opt.Ignore());
        
        CreateMap<VetService, VetServiceDto>().ReverseMap();
        
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => $"{src.Owner.FirstName} {src.Owner.LastName}"))
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.Animal.Name))
            .ForMember(dest => dest.VeterinarianName, opt => opt.MapFrom(src => $"{src.Veterinarian.FirstName} {src.Veterinarian.LastName}"))
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service != null ? src.Service.Name : string.Empty))
            .ForMember(dest => dest.ServicePrice, opt => opt.MapFrom(src => src.Service != null ? src.Service.Price : (decimal?)null))
            .ReverseMap()
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.Animal, opt => opt.Ignore())
            .ForMember(dest => dest.Veterinarian, opt => opt.Ignore())
            .ForMember(dest => dest.Service, opt => opt.Ignore());
        
        CreateMap<MedicalRecord, MedicalRecordDto>()
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.Animal.Name))
            .ForMember(dest => dest.VeterinarianName, opt => opt.MapFrom(src => $"{src.Veterinarian.FirstName} {src.Veterinarian.LastName}"))
            .ReverseMap()
            .ForMember(dest => dest.Animal, opt => opt.Ignore())
            .ForMember(dest => dest.Veterinarian, opt => opt.Ignore());
        
        CreateMap<Vaccination, VaccinationDto>()
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.Animal.Name))
            .ForMember(dest => dest.VeterinarianName, opt => opt.MapFrom(src => src.Veterinarian != null ? $"{src.Veterinarian.FirstName} {src.Veterinarian.LastName}" : string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.Animal, opt => opt.Ignore())
            .ForMember(dest => dest.Veterinarian, opt => opt.Ignore());
    }
}
