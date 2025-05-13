namespace AuthService.Application.Common.Exceptions;

[Serializable]
public class BadRequestException(string message) : Exception(message);
