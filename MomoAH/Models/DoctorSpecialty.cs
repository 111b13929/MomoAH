namespace MomoAH.Models
{
    /// <summary>
    /// 醫師與科別的關聯模型
    /// </summary>
    public class DoctorSpecialty
    {
        public required string DoctorId { get; set; }
        public required string SpecialtyId { get; set; }
    }
}
