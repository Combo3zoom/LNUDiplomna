namespace LightsOn.Domain.Exceptions;

public class NullClientException : Exception
{
    public NullClientException()
        : base("Null client error occured.") { }
}