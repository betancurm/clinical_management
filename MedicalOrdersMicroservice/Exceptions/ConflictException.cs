using System;

namespace MedicalOrdersMicroservice.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}