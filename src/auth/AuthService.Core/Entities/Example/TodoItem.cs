using AuthService.DataAccess.Common;

namespace AuthService.DataAccess.Entities.Example;

public class TodoItem : BaseEntity, IAuditedEntity
{
    public string Title { get; set; }

    public string Body { get; set; }

    public bool IsDone { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
