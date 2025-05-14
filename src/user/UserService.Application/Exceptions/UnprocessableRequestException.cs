namespace UserService.Application.Exceptions;

[Serializable]
public class UnprocessableRequestException : Exception
{
    public UnprocessableRequestException(string message) : base(message) { }
}
