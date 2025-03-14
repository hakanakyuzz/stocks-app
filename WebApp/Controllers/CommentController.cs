using Microsoft.AspNetCore.Mvc;
using WebApp.Dtos.Comment;
using WebApp.Interfaces;
using WebApp.Mappers;
using WebApp.Models;

namespace WebApp.Controllers;

[Route("api/comment")]
[ApiController]

public class CommentController : ControllerBase
{
    private readonly ICommentRepository _repository;

    public CommentController(ICommentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _repository.GetAllAsync();
        var commentDtos = comments.Select(comment => comment.ToCommentDto());
        
        return Ok(commentDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var comment = await _repository.GetByIdAsync(id);
        
        if (comment == null)
            return NotFound();
        
        return Ok(comment.ToCommentDto());
    }

    [HttpPost("")]
    public async Task<IActionResult> Create(CreateCommentDto commentDto)
    {
        var commentModel = commentDto.ToCommentFromDto();
        await _repository.CreateAsync(commentModel);
        
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto commentDto)
    {
        var commentModel = await _repository.UpdateAsync(id, commentDto);
        
        if (commentModel == null)
            return NotFound();
        
        return Ok(commentModel.ToCommentDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var commentModel = await _repository.DeleteAsync(id);
        
        if (commentModel == null)
            return NotFound();

        return NoContent();
    }
}