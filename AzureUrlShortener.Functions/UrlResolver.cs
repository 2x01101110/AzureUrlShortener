using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AzureUrlShortener.Functions.Models;
using AzureUrlShortener.Functions.Extensions;

namespace AzureUrlShortener.Functions
{
    public static class UrlResolver
    {
        [FunctionName("resolve")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{hashId}")]HttpRequest req,
            string hashId,
            [CosmosDB(
                databaseName: "url-db", 
                collectionName: "urls",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{hashId}"
            )]dynamic document,
            ILogger log)
        {
            log.LogInformation($"\r\nRequest received from [{req.ClientIP()}] with URL ID [{hashId}]");

            if (document == null) 
            {
                log.LogError($"Matching record not found.\r\n");
                return new BadRequestResult();
            }

            log.LogInformation($"Redirecting client to [{document.longUrl}]\r\n");

            return new RedirectResult(document.longUrl);
        }
    }
}
