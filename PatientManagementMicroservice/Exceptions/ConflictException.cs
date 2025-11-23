using System;

namespace PatientManagementMicroservice.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}