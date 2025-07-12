using Data;
using Dotnet_backend.Dtos.Comment;
using Dotnet_backend.Interfaces;
using Dotnet_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<object?> DeleteComment(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return null;
            }
            return comment;
        }

        public async Task<List<Comment>> GetComments()
        {
            return await _context.Comments.ToListAsync();

        }

        public async Task<List<Comment>?> GetCommentsByStockId(int id)
        {
            var comments = await _context.Comments.Where(x => x.StockId == id).ToListAsync();

            if (comments.Count == 0)
            {
                return null;
            }

            return comments;
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