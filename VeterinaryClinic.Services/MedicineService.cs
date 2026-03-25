using System;
using System.Collections.Generic;
using System.Linq;

namespace VeterinaryClinic.Services
{
    public class MedicineService
    {
        private List<string> medicines;

        public MedicineService()
        {
            medicines = new List<string>();
        }

        public void AddMedicine(string medicine)
        {
            if (!string.IsNullOrEmpty(medicine) && !medicines.Contains(medicine))
                medicines.Add(medicine);
        }

        public void RemoveMedicine(string medicine)
        {
            if (medicines.Contains(medicine))
                medicines.Remove(medicine);
        }

        public List<string> GetAllMedicines()
        {
            return medicines.ToList();
        }

        public bool IsMedicineAvailable(string medicine)
        {
            return medicines.Contains(medicine);
        }
    }
}