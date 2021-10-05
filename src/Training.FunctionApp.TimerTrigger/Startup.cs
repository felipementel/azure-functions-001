using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Training.FunctionApp.TimerTrigger;

[assembly: FunctionsStartup(typeof(Training.FunctionApp.HttpTrigger.Startup))]
namespace Training.FunctionApp.HttpTrigger
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddSingleton<WebRequestFelipe>()
                .AddHttpClient<WebRequestFelipe>("Avanade", config =>
           {
               config.BaseAddress = new System.Uri("https://jsonplaceholder.typicode.com");

               config.DefaultRequestHeaders.Clear();
               config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               config.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
           });
        }
    }
}