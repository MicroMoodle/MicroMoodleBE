using UserService.Core.Common;

namespace UserService.Core.Entities.Example;

public class TodoItem : BaseEntity, IAuditedEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public bool IsDone { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
