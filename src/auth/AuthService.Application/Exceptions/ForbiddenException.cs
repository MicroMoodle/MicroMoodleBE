using System;

namespace IMDB_BE.Exceptions;

[Serializable]
public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message) { }
}