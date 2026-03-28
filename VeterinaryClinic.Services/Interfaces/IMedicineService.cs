using System.Collections.Generic;
using VeterinaryClinic.Shared.DTOs;

namespace VeterinaryClinic.Services.Interfaces
{
    public interface IMedicineService
    {
        void AddMedicine(MedicineDto medicine);
        MedicineDto GetMedicineById(int id);
        IEnumerable<MedicineDto> GetAllMedicines();
        void UpdateMedicine(MedicineDto medicine);
        void DeleteMedicine(int id);
    }
}
