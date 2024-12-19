using System.ComponentModel.DataAnnotations.Schema;

namespace MomoAH.Models
{
    public class BusinessHours
    {
        [Column("day_of_week")]
        public required string DayOfWeek { get; set; }

        [Column("morning_shift")]
        public TimeSpan? MorningShift { get; set; }

        [Column("morning_end")]
        public TimeSpan? MorningEnd { get; set; }

        [Column("afternoon_shift")]
        public TimeSpan? AfternoonShift { get; set; }

        [Column("afternoon_end")]
        public TimeSpan? AfternoonEnd { get; set; }

        [Column("evening_shift")]
        public TimeSpan? EveningShift { get; set; }

        [Column("evening_end")]
        public TimeSpan? EveningEnd { get; set; }
    }
}
