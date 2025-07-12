using Data;
using Dotnet_backend.Dtos.Comment;
using Dotnet_backend.Interfaces;
using Dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_backend.Repositories
{
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Comment?> CreateAsync(Comment comment)
        {
            var stock = await _context.Stocks.FindAsync(comment.StockId);
            if (stock == null)
            {
                return null;
            }
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequest updateCommentRequest)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (comment == null)
            {
                return null;
            }

            comment.Title = updateCommentRequest.Title;
            comment.Content = updateCommentRequest.Content;

            await _context.SaveChangesAsync();
            return comment;
        }
    }
}