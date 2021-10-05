using System;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Training.FunctionApp.TimerTrigger
{
    public class Function1
    {
        private readonly WebRequestFelipe _webRequestFelipe;

        public Function1(WebRequestFelipe webRequestFelipe)
        {
            _webRequestFelipe = webRequestFelipe;
        }

        [FunctionName("Function1")]
        public void Run([TimerTrigger("*/5 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var lista = _webRequestFelipe.GetAllPosts().Take(10);

            foreach (var item in lista)
            {
                log.LogInformation($"Id: {item.Id}");
            }
        }
    }
}
