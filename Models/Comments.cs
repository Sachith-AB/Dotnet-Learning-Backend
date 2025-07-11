using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_backend.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]  // Title length
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "text")]  // Use 'text' for potentially long content
        public string Content { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]  // Explicit datetime type
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? StockId { get; set; }

        [ForeignKey("StockId")]
        public Stock? Stock { get; set; }
    }
}