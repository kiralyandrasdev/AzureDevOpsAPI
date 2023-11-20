using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace AzureDevOps.API.Settings
{
    public class ApplicationJsonSerializerSettings : JsonSerializerSettings
    {
        public ApplicationJsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore;
            ContractResolver = new CamelCasePropertyNamesContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    ProcessDictionaryKeys = true,
                    OverrideSpecifiedNames = true
                }
            };
        }
    }
}
