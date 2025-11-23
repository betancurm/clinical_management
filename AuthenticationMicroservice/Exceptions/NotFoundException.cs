using System;

namespace AuthenticationMicroservice.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}