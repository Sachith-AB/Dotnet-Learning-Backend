using Dotnet_backend.Dtos.Comment;
using Dotnet_backend.Models;

namespace Dotnet_backend.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment?> CreateAsync(Comment comment);

        Task<Comment?> UpdateAsync(int id, UpdateCommentRequest updateCommentRequest);

        Task<Comment?> GetCommentById(int id);

        Task<List<Comment>> GetComments();

        Task<object?> DeleteComment(int id);
    }
}