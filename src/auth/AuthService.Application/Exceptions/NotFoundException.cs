using System;

namespace IMDB_BE.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}