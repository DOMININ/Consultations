using System;
using System.ComponentModel.DataAnnotations;

namespace myProject.Models
{
    public class Consultation
    {
        public int consultationId { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime time { get; set; }
        public string symptoms { get; set; }

        public int patientId { get; set; }
        public virtual Patient patient { get; set; }
    }
}