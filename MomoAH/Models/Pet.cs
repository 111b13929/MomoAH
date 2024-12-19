namespace MomoAH.Models
{
    public class Pet
    {
        public string PetId { get; set; } = Guid.NewGuid().ToString(); // 預設生成 GUID
        public required string Name { get; set; }
        public required string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public required string Breed { get; set; }
        public required string OwnerId { get; set; }
    }

}
