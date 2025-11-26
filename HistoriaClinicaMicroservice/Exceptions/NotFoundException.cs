using System;

namespace HistoriaClinicaMicroservice.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}