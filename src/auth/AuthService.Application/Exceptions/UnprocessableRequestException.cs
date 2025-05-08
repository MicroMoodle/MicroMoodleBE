using System;

namespace IMDB_BE.Exceptions;

[Serializable]
public class UnprocessableRequestException : Exception
{
    public UnprocessableRequestException(string message) : base(message) { }
}