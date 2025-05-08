using System;

namespace IMDB_BE.Exceptions;

[Serializable]
public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}