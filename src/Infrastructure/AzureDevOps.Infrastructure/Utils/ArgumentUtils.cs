using System;

namespace AzureDevOps.Infrastructure.Utils
{
    public static class ArgumentUtils
    {
        public static void ThrowIfNull(object argument, string argumentName)
        {
            if (argument is null)
                throw new ArgumentNullException(argumentName);
        }
    }
}
