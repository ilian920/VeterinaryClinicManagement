using System;
using System.Collections.Generic;
using System.Linq;

namespace VeterinaryClinic.Services
{
    public class PatientService
    {
        private List<Patient> patients = new List<Patient>();

        public void AddPatient(Patient patient)
        {
            patients.Add(patient);
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return patients;
        }

        public Patient GetPatientById(int id)
        {
            return patients.FirstOrDefault(p => p.Id == id);
        }

        public void RemovePatient(int id)
        {
            var patient = GetPatientById(id);
            if (patient != null)
            {
                patients.Remove(patient);
            }
        }
    }

    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string OwnerName { get; set; }
    }
}