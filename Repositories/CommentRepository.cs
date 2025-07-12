using Data;
using Dotnet_backend.Interfaces;
using Dotnet_backend.Models;

namespace Dotnet_backend.Repositories
{
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Comment?> CreateAsync(Comment comment)
        {
            var stock = await  _context.Stocks.FindAsync(comment.StockId);
            if (stock == null)
            {
                return null;
            }
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}