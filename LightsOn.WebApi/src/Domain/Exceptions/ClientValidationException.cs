namespace LightsOn.Domain.Exceptions;

public class ClientValidationException : Exception
{
    public ClientValidationException(Exception innerException) 
        : base("Client validation error occured, try again.", innerException) { }
}