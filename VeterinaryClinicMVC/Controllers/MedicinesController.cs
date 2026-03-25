using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace VeterinaryClinicMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinesController : ControllerBase
    {
        private static List<Medicine> medicines = new List<Medicine>();
        private static int nextId = 1;

        // GET: api/medicines
        [HttpGet]
        public ActionResult<List<Medicine>> GetAllMedicines()
        {
            return Ok(medicines);
        }

        // GET: api/medicines/{id}
        [HttpGet("{id}")]
        public ActionResult<Medicine> GetMedicine(int id)
        {
            var medicine = medicines.FirstOrDefault(m => m.Id == id);
            if (medicine == null)
                return NotFound();
            return Ok(medicine);
        }

        // POST: api/medicines
        [HttpPost]
        public ActionResult<Medicine> AddMedicine(Medicine medicine)
        {
            medicine.Id = nextId++;
            medicines.Add(medicine);
            return CreatedAtAction(nameof(GetMedicine), new { id = medicine.Id }, medicine);
        }

        // PUT: api/medicines/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateMedicine(int id, Medicine medicine)
        {
            var existingMedicine = medicines.FirstOrDefault(m => m.Id == id);
            if (existingMedicine == null)
                return NotFound();

            existingMedicine.Name = medicine.Name;
            existingMedicine.Quantity = medicine.Quantity;
            existingMedicine.Price = medicine.Price;
            return NoContent();
        }

        // DELETE: api/medicines/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteMedicine(int id)
        {
            var medicine = medicines.FirstOrDefault(m => m.Id == id);
            if (medicine == null)
                return NotFound();

            medicines.Remove(medicine);
            return NoContent();
        }
    }

    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}