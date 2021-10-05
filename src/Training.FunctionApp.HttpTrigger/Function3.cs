using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Training.FunctionApp.HttpTrigger
{
    public static class Function3
    {
        //        public static async Task<IActionResult> Run(
        //    [HttpTrigger(AuthorizationLevel.Anonymous,"get", "post", Route = null)]
        //    HttpRequest req)
        //    [CosmosDB(databaseName: "WordCounts",
        //        collectionName: "Items",
        //        ConnectionStringSetting = "CosmosDBConnection",
        //        CreateIfNotExists = true)]
        //        ICollector<doc> docs)
        //{

        //            }

        //"Hello/{id:int?}"
        [FunctionName("Function3")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "function3")]
                HttpRequest req,
            [CosmosDB(
                databaseName: "ToDoItems",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDB_ConnectionString",
                CreateIfNotExists = true,
                Id = "{Query.id}",
                PartitionKey = "{Query.partitionKey}")] ToDoItem toDoItem,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (toDoItem == null)
            {
                log.LogInformation($"ToDo item not found");
            }
            else
            {
                log.LogInformation($"Found ToDo item, Description={toDoItem.Description}");
            }
            return new OkResult();
        }
    }

    public class ToDoItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }

        public string Description { get; set; }
    }

    public class ToDoItemLookup
    {
        public string ToDoItemId { get; set; }

        public string ToDoItemPartitionKeyValue { get; set; }
    }
}
