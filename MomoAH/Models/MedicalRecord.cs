using System.ComponentModel.DataAnnotations.Schema;

namespace MomoAH.Models
{
    public class MedicalRecord
    {
        [Column("record_id")]
        public string RecordId { get; set; } = null!;

        [Column("pet_id")]
        public string PetId { get; set; } = null!;

        [Column("specialty_id")]
        public string? SpecialtyId { get; set; }

        [Column("doctor_id")]
        public string? DoctorId { get; set; }

        [Column("pet_name")]
        public string PetName { get; set; } = null!;

        [Column("visit_date")]
        public DateTime VisitDate { get; set; }

        [Column("visit_notes")]
        public string? VisitNotes { get; set; } // 允許 null 值
    }
}
