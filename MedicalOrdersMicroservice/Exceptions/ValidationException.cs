using System;

namespace MedicalOrdersMicroservice.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}