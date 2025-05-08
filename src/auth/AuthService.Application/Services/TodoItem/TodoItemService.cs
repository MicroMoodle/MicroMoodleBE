using AuthService.Application.Models.Example.TodoItem;
using AuthService.Core.Repositories;
using AutoMapper;

namespace AuthService.Application.Services.TodoItem;

public class TodoItemService : ITodoItemService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TodoItemService(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateTodoItemResponseModel> CreateAsync(CreateTodoItemModel createTodoItemModel,
        CancellationToken cancellationToken = default)
    {
        var todoItem = _mapper.Map<Core.Entities.Example.TodoItem>(createTodoItemModel);

        return new CreateTodoItemResponseModel
        {
            Id = (await _unitOfWork.Repository<Core.Entities.Example.TodoItem>().AddAsync(todoItem)).Id
        };
    }
}
