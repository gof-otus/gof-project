﻿namespace Finik.Infrastructure.Exceptions;

public class NewsException : Exception
{
    public NewsException() : base() { }

    public NewsException(string message) : base(message) { }
}
