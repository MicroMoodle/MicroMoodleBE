using UserService.Application.Models.Example.TodoItem;

namespace UserService.Application.Services.TodoItem;

public interface ITodoItemService
{
    Task<CreateTodoItemResponseModel> CreateAsync(CreateTodoItemModel createTodoItemModel,
        CancellationToken cancellationToken = default);

}
