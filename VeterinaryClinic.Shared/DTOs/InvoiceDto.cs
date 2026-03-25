using System;
using System.Collections.Generic;

namespace VeterinaryClinic.Shared.DTOs
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid PetId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<string> ServicesRendered { get; set; }
        public bool IsPaid { get; set; }
    }
}