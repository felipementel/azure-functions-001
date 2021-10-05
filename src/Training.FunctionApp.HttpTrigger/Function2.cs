using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Training.FunctionApp.HttpTrigger.Repositories;

namespace Training.FunctionApp.HttpTrigger
{
    public class Function2
    {
        private readonly AzureSqlRepository _azureSqlRepository;

        public Function2(AzureSqlRepository azureSqlRepository)
        {
            _azureSqlRepository = azureSqlRepository;
        }

        [FunctionName("Function2")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var data = System.Text.Json.JsonSerializer.Deserialize<UserSql>(requestBody,
                new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (req.Method == HttpMethods.Post)
            {
                _azureSqlRepository.SaveDapper(data);
                _azureSqlRepository.SaveADO(data);

                return new OkObjectResult("Saved");
            }
            else if (req.Method == HttpMethods.Get)
            {
                var itens = _azureSqlRepository.GetAll();

                return new OkObjectResult(itens);
            }
            else
            {
                return new NotFoundResult();
            }
        }
    }

    public class UserSql
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}