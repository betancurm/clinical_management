using System;

namespace InventoryMicroservice.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}