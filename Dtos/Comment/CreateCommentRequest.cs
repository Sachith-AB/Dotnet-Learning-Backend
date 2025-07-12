namespace Dotnet_backend.Dtos.Comment
{
    public class CreateCommentRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int? StockId { get; set; }
    }
}