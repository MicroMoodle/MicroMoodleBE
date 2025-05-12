using AuthService.Application.TodoItems.Commands;
using AuthService.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using AuthService.Core.Entities.Example;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

public class TodoItemController(ISender sender) : ApiController
{
    [HttpGet("todoitems")]
    public async Task<ObjectResult> GetTodoItems(
        [FromQuery] GetTodoItemsWithPaginationQuery query)
    {
        var result = await sender.Send(query);
        return Ok(result);
    }

    [HttpPost("todoitems")]
    public async Task<ObjectResult> CreateTodoItem(
        [FromBody] CreateTodoItemCommand command)
    {
        var result = await sender.Send(command);
        return Created($"/{nameof(TodoItemController)}/{result}", result);
    }
}
