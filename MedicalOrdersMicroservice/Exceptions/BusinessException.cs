using System;

namespace MedicalOrdersMicroservice.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}