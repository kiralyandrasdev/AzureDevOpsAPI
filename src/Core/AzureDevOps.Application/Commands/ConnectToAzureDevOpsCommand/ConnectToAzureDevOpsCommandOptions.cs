using System.ComponentModel.DataAnnotations;

namespace AzureDevOps.Application.Commands.ConnectToAzureDevOpsCommand
{
    public class ConnectToAzureDevOpsCommandOptions
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public string AccessToken { get; set; }
    }
}
