namespace AuthService.Core.Common;

public abstract class BaseAuditedEntity : BaseEntity
{
    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
