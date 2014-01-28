namespace PrivateClinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        public string Address { get; set; }

        [Display(Name = "Date of last visit")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOLV { get; set; }

        public virtual ICollection<PatientDoc> PatientDocuments { get; set; }

    }
}