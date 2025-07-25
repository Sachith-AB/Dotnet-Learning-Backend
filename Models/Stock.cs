using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_backend.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Symbol { get; set; } = string.Empty;

        [Column(TypeName = "varchar(255)")]
        public string CompanyName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
        
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    }
}
