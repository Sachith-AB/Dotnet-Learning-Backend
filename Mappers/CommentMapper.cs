using Dotnet_backend.Dtos.Comment;
using Dotnet_backend.Models;

namespace Dotnet_backend.Mappers
{
    public static class CommentMapper
    {
        public static Comment ToCommentFromCreateDto(this CreateCommentRequest createCommentRequest)
        {
            return new Comment
            {
                Title = createCommentRequest.Title,
                Content = createCommentRequest.Content,
                StockId = createCommentRequest.StockId,
            };
        }
    }
}