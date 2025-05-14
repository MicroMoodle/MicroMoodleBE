using UserService.Application.Models.Example.TodoItem;
using UserService.Core.Entities.Example;
using AutoMapper;

namespace UserService.Application.MappingProfiles.Example;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<CreateTodoItemModel, TodoItem>()
            .ForMember(ti => ti.IsDone, ti => ti.MapFrom(cti => false));
    }
}
