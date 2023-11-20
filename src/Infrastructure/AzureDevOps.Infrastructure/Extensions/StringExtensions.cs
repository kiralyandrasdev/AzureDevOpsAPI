using HtmlAgilityPack;
using System.Net;

namespace AzureDevOps.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string SanitizeHtmlString(this string htmlString)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            return WebUtility.HtmlDecode(doc.DocumentNode.InnerText);
        }
    }
}
