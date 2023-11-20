using System;

namespace AzureDevOps.API.Exceptions
{
    public class AuthorizationFailedException : Exception
    {
        public AuthorizationFailedException(string message) : base(message) { }
    }
}
