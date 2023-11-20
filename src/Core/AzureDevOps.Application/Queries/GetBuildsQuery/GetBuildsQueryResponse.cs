using System.Collections.Generic;
using AzureDevOps.Domain.Entities;
using AzureDevOps.Domain.Response;

namespace AzureDevOps.Application.Queries.GetBuildsQuery
{
    public class GetBuildsQueryResponse : BaseResponse
    {
        public IEnumerable<Build> Value { get; set; }
    }
}
