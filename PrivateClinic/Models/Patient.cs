namespace PrivateClinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;

    public class Patient
    {
        public int ID { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [StringLength(30)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        [UIHint("PostalAddressTemplate")]
        public string Address { get; set; }
    }

    public class PatientContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
    }
}