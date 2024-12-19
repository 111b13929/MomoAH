using System.ComponentModel.DataAnnotations.Schema;

namespace MomoAH.Models
{
    public class Doctor
    {
        [Column("doctor_id")]
        public string? DoctorId { get; set; } // 使用 '?' 表示可為 null

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("gender")]
        public string Gender { get; set; } = string.Empty;

        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [Column("hire_date")]
        public DateTime HireDate { get; set; }
    }
}
