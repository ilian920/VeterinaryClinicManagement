using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data
{
    public class VeterinaryClinicContext : DbContext
    {
        public VeterinaryClinicContext(DbContextOptions<VeterinaryClinicContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        // Add other DbSets for your entities here

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity properties and relationships
            base.OnModelCreating(modelBuilder);
        }
    }
}