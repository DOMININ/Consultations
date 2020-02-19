using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myProject.Models
{
    public class Patient
    {
        public int patientId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public DateTime birthDate { get; set; }
        public string gender { get; set; }
        public string SNILS { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }
    }
}
