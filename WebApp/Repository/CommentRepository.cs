using Microsoft.EntityFrameworkCore;
using WebApp.Database;
using WebApp.Dtos.Comment;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repository;

public class CommentRepository : ICommentRepository
{
    // _context has direct connection to database
    // -context is the EF Core's gateway to the database
    private readonly AppDbContext _context;
    
    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        // _context.Comment represents the Comment table in the database
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

    // Give me an id and an updated comment object (a type of UpdateCommentDto)
    // If the id exists, I'll return the updated comment
    // If not I'll return null
    public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto)
    {
        var commentModel = await _context.Comment.FirstOrDefaultAsync(comment => comment.Id == id);

        if (commentModel == null)
            return null;
        
        commentModel.Title = commentDto.Title;
        commentModel.Content = commentDto.Content;
        
        await _context.SaveChangesAsync();
        
        return commentModel;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var commentModel = await _context.Comment.FirstOrDefaultAsync(comment => comment.Id == id);

        if (commentModel == null)
            return null;
        
        // Remove() marks as deleted
        _context.Comment.Remove(commentModel);
        
        // SaveChangesAsync() executes the DELETE SQL
        await _context.SaveChangesAsync();
        
        // Here the object is still exists in memory namely commentModel which is a local variable
        // EF Core just removed it from the database - but the object itself still holds the data in memory
        return commentModel;
    }
}