namespace AuthService.Application.Common.Exceptions;

[Serializable]
public class ForbiddenException(string message) : Exception(message);
