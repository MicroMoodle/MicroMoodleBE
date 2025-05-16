using AuthService.Application.Common.Interfaces;
using AuthService.Core.Entities.Example;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class GetTodoItemsWithPaginationQuery : IRequest<List<TodoItemDTO>>
{
}

public class GetTodoItemsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetTodoItemsWithPaginationQuery, List<TodoItemDTO>>
{
    public async Task<List<TodoItemDTO>> Handle(GetTodoItemsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var spec = GetTodoItemsSpecification.ApplyOrderByTitle();
        return await unitOfWork.Repository<TodoItem>().Find(spec)
            .ProjectTo<TodoItemDTO>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
