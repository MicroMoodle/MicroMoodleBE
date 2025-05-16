namespace AuthService.Application.Common.Exceptions;

[Serializable]
public class ValidationException(List<string> errors)
    : BadRequestException("One or more validation failures have occurred.")
{
    public List<string> Errors { get; } = errors.ToList();
}
