namespace AuthService.Core.Common;

public abstract class BaseAuditedEntity : BaseEntity
{
    public Guid CreatedBy { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTimeOffset? UpdatedOn { get; set; }
}
