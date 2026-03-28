using System;

namespace VeterinaryClinic.Shared.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Breed { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContact { get; set; }
    }
}
