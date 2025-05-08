using AuthService.Application.Models.Example.TodoItem;

namespace AuthService.Application.Services.TodoItem;

public interface ITodoItemService
{
    Task<CreateTodoItemResponseModel> CreateAsync(CreateTodoItemModel createTodoItemModel,
        CancellationToken cancellationToken = default);

}
