using Dotnet_backend.Dtos.Comment;
using Dotnet_backend.Models;

namespace Dotnet_backend.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto? ToCommentDto(this Comment? comment)
        {
            return comment == null ? null :
            new CommentDto
            {
                Id = comment.Id,
                CreatedOn = comment.CreatedOn,
                Title = comment.Title,
                Content = comment.Content,
                StockId = comment.StockId,
            };
        }
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