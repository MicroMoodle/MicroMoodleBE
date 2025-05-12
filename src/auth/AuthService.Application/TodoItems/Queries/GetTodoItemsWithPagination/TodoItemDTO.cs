using AuthService.Core.Entities.Example;
using AutoMapper;

namespace AuthService.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class TodoItemDTO
{
    public int Id { get; init; }

    public string Title { get; init; }

    public bool IsDone { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TodoItem, TodoItemDTO>();
        }
    }
}
