using System;

namespace HistoriaClinicaMicroservice.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}