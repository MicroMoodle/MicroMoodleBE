using AuthService.Core.Common;
using AuthService.Core.Events;

namespace AuthService.Core.Entities.Example;

public class TodoItem : BaseAuditedEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    private bool _isDone;
    public bool IsDone
    {
        get => _isDone;
        set
        {
            if (value && !_isDone)
            {
                AddDomainEvent(new TodoItemCompletedEvent(this));
            }

            _isDone = value;
        }
    }
}
