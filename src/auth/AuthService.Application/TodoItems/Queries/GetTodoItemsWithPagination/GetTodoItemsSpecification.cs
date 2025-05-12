using AuthService.Application.Common.Interfaces;
using AuthService.Application.Common.Specifications;
using AuthService.Core.Entities.Example;

namespace AuthService.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public static class GetTodoItemsSpecification
{
    public static BaseSpecification<TodoItem> ApplyOrderByTitle()
    {
        var spec = new BaseSpecification<TodoItem>(x => true);
        spec.ApplyOrderBy(x => x.Title);
        return spec;
    }
}
