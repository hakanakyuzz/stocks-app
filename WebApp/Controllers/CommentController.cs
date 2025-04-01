using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Dtos.Comment;
using WebApp.Extensions;
using WebApp.Interfaces;
using WebApp.Mappers;
using WebApp.Models;

namespace WebApp.Controllers;

[Route("api/comment")]
[ApiController]

public class CommentController : ControllerBase
{
    private readonly ICommentRepository _repository;
    private readonly IStockRepository _stockRepository;
    private readonly UserManager<User> _userManager;

    // The controller only knows about IStockRepository - not the implementation
    // The controller doesn't care if the repository uses EF Core or MongoDB
    // The controller just expects the repository to return the correct data
    public CommentController(ICommentRepository repository, IStockRepository stockRepository, UserManager<User> userManager)
    {
        // _repository is an instance of a class that implements the Repository Pattern (IStockRepository)
        // _repository is a wrapper around _context that adds an extra layer of abstraction
        
        _repository = repository;
        _stockRepository = stockRepository;
        _userManager = userManager;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _repository.GetAllAsync();
        var commentDtos = comments.Select(comment => comment.ToCommentDto());
        
        return Ok(commentDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await _repository.GetByIdAsync(id);
        
        if (comment == null)
            return NotFound();
        
        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!await _stockRepository.StockExists(stockId))
            return NotFound();

        var username = User.GetUserName();
        var user = await _userManager.FindByNameAsync(username);
        
        var commentModel = commentDto.ToCommentFromDto(stockId);
        commentModel.UserId = user.Id;
        
        await _repository.CreateAsync(commentModel);
        
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var commentModel = await _repository.UpdateAsync(id, commentDto);
        
        if (commentModel == null)
            return NotFound();
        
        return Ok(commentModel.ToCommentDto());
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var commentModel = await _repository.DeleteAsync(id);
        
        if (commentModel == null)
            return NotFound();

        return NoContent();
    }
}