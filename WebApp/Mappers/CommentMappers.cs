using WebApp.Dtos.Comment;
using WebApp.Models;

namespace WebApp.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto()
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId,
        };
    }

    public static Comment ToCommentFromDto(this CreateCommentDto commentDto)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            CreatedOn = commentDto.CreatedOn,
            StockId = commentDto.StockId,
        };
    }
}