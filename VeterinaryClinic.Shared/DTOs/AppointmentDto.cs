// AppointmentDto.cs

namespace VeterinaryClinic.Shared.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string PatientName { get; set; }
        public string Veterinarian { get; set; }
        public string Notes { get; set; }
    }
}