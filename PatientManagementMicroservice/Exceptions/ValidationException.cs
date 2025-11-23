using System;

namespace PatientManagementMicroservice.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}