using System;

namespace AuthenticationMicroservice.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}