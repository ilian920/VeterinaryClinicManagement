namespace VeterinaryClinic.Services.Interfaces
{
    public interface IMedicineService
    {
        // Add method signatures for medicine-related operations
        void AddMedicine(Medicine medicine);
        Medicine GetMedicineById(int id);
        IEnumerable<Medicine> GetAllMedicines();
        void UpdateMedicine(Medicine medicine);
        void DeleteMedicine(int id);
    }
}