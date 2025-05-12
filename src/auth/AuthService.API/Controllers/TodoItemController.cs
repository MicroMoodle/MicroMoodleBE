using AuthService.Application.TodoItems.Commands;
using AuthService.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using AuthService.Core.Entities.Example;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

public class TodoItemController : ApiController
{
    [HttpGet("todoitems")]
    public async Task<Ok<List<TodoItemDTO>>> GetTodoItems(ISender sender,
        [AsParameters] GetTodoItemsWithPaginationQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    [HttpPost("todoitems")]
    public async Task<Created<Guid>> CreateTodoItem(
        ISender sender, CreateTodoItemCommand command)
    {
        var result = await sender.Send(command);
        return TypedResults.Created($"/{nameof(TodoItemController)}/{result}", result);
    }
}
