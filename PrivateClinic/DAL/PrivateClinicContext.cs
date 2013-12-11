namespace PrivateClinic.DAL
{
    using PrivateClinic.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;

    public class PrivateClinicContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
    }
}