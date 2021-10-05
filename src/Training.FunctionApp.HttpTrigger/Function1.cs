using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Training.FunctionApp.HttpTrigger.Repositories;

namespace Training.FunctionApp.HttpTrigger
{
    public class Function1
    {
        private readonly CosmosDbRepository _cosmosDbRepository;

        public Function1(CosmosDbRepository cosmosDbRepository)
        {
            _cosmosDbRepository = cosmosDbRepository;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "function1")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UserMongo>(requestBody);

            log.LogInformation($"Request Method: {req.Method}");


            if (req.Method == HttpMethods.Post)
            {
                _cosmosDbRepository.Save(data);
                return new OkObjectResult("Saved");
            }
            else if (req.Method == HttpMethods.Get)
            {
                var itens = _cosmosDbRepository.GetAll();
                return new OkObjectResult(itens);
            }
            else
            {
                return new NotFoundResult();
            }        
        }
    }

    [BsonIgnoreExtraElements]
    public class UserMongo
    {
        [BsonId]
        public BsonObjectId id { get; set; }
        public string name { get; set; }
    }
}
