using System.ComponentModel.DataAnnotations.Schema;

namespace MomoAH.Models
{
    public class Schedule
    {
        [Column("schedule_id")]
        public string ScheduleId { get; set; } = null!;

        [Column("date")]
        public DateTime? Date { get; set; }

        [Column("day_of_week")]
        public string? DayOfWeek { get; set; }

        [Column("shift_time")]
        public string? ShiftTime { get; set; }

        [Column("doctor_id")]
        public string? DoctorId { get; set; }

        [Column("doctor_name")]
        public string? DoctorName { get; set; }

        [Column("specialty_id")]
        public string? SpecialtyId { get; set; }

        [Column("specialty_name")]
        public string? SpecialtyName { get; set; }
    }
}
