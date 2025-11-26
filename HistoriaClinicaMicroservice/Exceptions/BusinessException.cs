using System;

namespace HistoriaClinicaMicroservice.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}