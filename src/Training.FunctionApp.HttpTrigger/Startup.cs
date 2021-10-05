using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Training.FunctionApp.HttpTrigger.Repositories;

[assembly: FunctionsStartup(typeof(Training.FunctionApp.HttpTrigger.Startup))]
namespace Training.FunctionApp.HttpTrigger
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<CosmosDbRepository>();
            builder.Services.AddSingleton<AzureSqlRepository>();
        }
    }
}
