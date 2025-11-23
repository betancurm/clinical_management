using System;

namespace PatientManagementMicroservice.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}