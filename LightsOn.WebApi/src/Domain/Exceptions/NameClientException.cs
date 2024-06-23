namespace LightsOn.Domain.Exceptions;

public class NameClientException : Exception
{
    public NameClientException() : base("Name should contain less 200 symbols") {}
}