namespace AuthService.Application.Common.Exceptions;

[Serializable]
public class UnprocessableRequestException(string message) : Exception(message);
