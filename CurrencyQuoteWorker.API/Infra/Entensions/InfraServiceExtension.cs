using CurrencyQuoteWorker.API.Infra.Interfaces.Services.Queue;
using CurrencyQuoteWorker.API.Infra.Services.Queue;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyQuoteWorker.API.Infra.Entensions
{
    public static class InfraServiceExtension
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddScoped<IQueueService, QueueService>();

            return services;
        }
    }
}
