using Microsoft.EntityFrameworkCore;
using WebApp.Database;
using WebApp.Dtos.Comment;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;
    
    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comment.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        var commentModel = await _context.Comment.FirstOrDefaultAsync(comment => comment.Id == id);

        return commentModel ?? null;
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comment.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        
        return commentModel;
    }

    public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto)
    {
        var commentModel = await _context.Comment.FirstOrDefaultAsync(comment => comment.Id == id);

        if (commentModel == null)
            return null;
        
        commentModel.Title = commentDto.Title;
        commentModel.Content = commentDto.Content;
        commentModel.CreatedOn = commentDto.CreatedOn;
        
        await _context.SaveChangesAsync();
        
        return commentModel;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var commentModel = await _context.Comment.FirstOrDefaultAsync(comment => comment.Id == id);

        if (commentModel == null)
            return null;
        
        _context.Comment.Remove(commentModel);
        await _context.SaveChangesAsync();
        
        return commentModel;
    }
}