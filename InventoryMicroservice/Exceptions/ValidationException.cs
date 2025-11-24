using System;

namespace InventoryMicroservice.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}