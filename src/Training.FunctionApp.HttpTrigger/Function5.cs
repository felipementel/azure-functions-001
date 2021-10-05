using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Training.FunctionApp.HttpTrigger
{
    public static class HttpTriggerCSharp1
    {
        //[FunctionName("HttpTriggerCSharp1")]
        //public static async Task<IActionResult> Run(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "employees")] HttpRequest req,
        //    ILogger log,
        //    [Sql("select * from Employees",
        //CommandType = System.Data.CommandType.Text,
        //ConnectionStringSetting = "SqlConnectionString")]
        //IEnumerable<string> employee)
        //{
        //    return new OkObjectResult(employee);
        //}
    }
}
