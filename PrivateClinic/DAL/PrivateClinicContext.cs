namespace PrivateClinic.DAL
{
    using PrivateClinic.Models;
    using System.Data.Entity;

    public class PrivateClinicContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientDoc> PatientDocuments { get; set; }
    }
}