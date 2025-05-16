using AuthService.Core.Common;
using AuthService.Core.Entities.Example;

namespace AuthService.Core.Events;

public class TodoItemCompletedEvent(TodoItem item) : BaseEvent
{
    public TodoItem Item { get; } = item;
}
