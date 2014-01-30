namespace PrivateClinic.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PatientDoc
    {
        public int ID { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Date of visit")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Diagnosis { get; set; }

        public string Symptom { get; set; }

        public string Lab { get; set; }

        public string Treatment { get; set; }

        public string Result { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Fee { get; set; }

        public virtual Patient Patient { get; set; }
    }
}