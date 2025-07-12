using Dotnet_backend.Models;

namespace Dotnet_backend.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment?> CreateAsync(Comment comment);
    }
}