using AuthService.Application.Models.Example.TodoItem;
using AuthService.Core.Entities.Example;
using AutoMapper;

namespace AuthService.Application.MappingProfiles.Example;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<CreateTodoItemModel, TodoItem>()
            .ForMember(ti => ti.IsDone, ti => ti.MapFrom(cti => false));
    }
}
