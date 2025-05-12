using AuthService.Application.Common.Interfaces;
using AuthService.Core.Entities.Example;
using AuthService.Core.Events;
using MediatR;

namespace AuthService.Application.TodoItems.Commands;

public record CreateTodoItemCommand : IRequest<Guid>
{
    public string Title { get; init; }
}

public class CreateTodoItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTodoItemCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItem
        {
            Title = request.Title,
            IsDone = false
        };

        entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await unitOfWork.Repository<TodoItem>().AddAsync(entity);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
