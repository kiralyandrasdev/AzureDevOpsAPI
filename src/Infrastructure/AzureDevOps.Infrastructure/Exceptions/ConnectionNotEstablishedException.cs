using System;

namespace AzureDevOps.Infrastructure.Exceptions
{
    public class ConnectionNotEstablishedException : Exception
    {
        public ConnectionNotEstablishedException(string message) : base(message) { }
    }
}
