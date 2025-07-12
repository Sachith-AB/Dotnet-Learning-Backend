namespace Dotnet_backend.Dtos.Comment
{
    public class UpdateCommentRequest
    {
        public string Title { get; set; } = string.Empty;
        
        public string Content { get; set; } = string.Empty;
    }
}