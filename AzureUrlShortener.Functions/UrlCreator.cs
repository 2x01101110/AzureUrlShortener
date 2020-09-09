using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;
using AzureUrlShortener.Functions.Models;
using System;
using System.Web.Http;
using AzureUrlShortener.Functions.Helpers;

namespace AzureUrlShortener.Functions
{
    public static class UrlCreator
    {
        [FunctionName("create")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req,
            [CosmosDB(ConnectionStringSetting = "CosmosDBConnection")]DocumentClient client,
            ILogger log)
        {
            try
            {
                var modelString = await new StreamReader(req.Body).ReadToEndAsync();
                var model = JsonConvert.DeserializeObject<UrlRecordCreateModel>(modelString);

                log.LogInformation($"Shortening {model.LongUrl}");

                var result = await client.CreateDocumentAsync(
                    UriFactory.CreateDocumentCollectionUri(databaseId: "url-db", collectionId: "urls"),
                    document: new 
                    { 
                        id = HashFromLongUrl.Create(model.LongUrl),
                        longUrl = model.LongUrl
                    });

                return new OkObjectResult(new
                {
                    shortUrl = $"{Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")}/{result.Resource.Id}"
                });
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new InternalServerErrorResult();
            }
        }
    }
}
