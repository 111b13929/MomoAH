using System.ComponentModel.DataAnnotations;

namespace MomoAH.Models
{
    /// <summary>
    /// Owner 類別代表飼主的基本資料，對應到 MomoAHDB 資料庫的 Owner 表格。
    /// </summary>
    public class Owner
    {
        [Key]
        public string OwnerId { get; set; } = Guid.NewGuid().ToString(); // 自動生成 ID
        public string? Name { get; set; } // 可為空
        public string? Gender { get; set; } // 可為空
        public string? Phone { get; set; } // 可為空
        public string? Address { get; set; } // 可為空
    }

}
