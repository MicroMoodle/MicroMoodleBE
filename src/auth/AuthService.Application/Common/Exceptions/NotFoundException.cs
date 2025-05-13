namespace AuthService.Application.Common.Exceptions;

[Serializable]
public class NotFoundException(string message) : Exception(message);
