using System;

namespace LightsOn.Domain.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(uint clientId)
            : base($"Client with ID {clientId} was not found.")
        {
        }
    }
}