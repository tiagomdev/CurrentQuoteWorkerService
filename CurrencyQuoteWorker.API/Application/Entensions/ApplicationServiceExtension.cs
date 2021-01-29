using CurrencyQuoteWorker.API.Application.Interfaces.Messages;
using CurrencyQuoteWorker.API.Application.Services.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyQuoteWorker.API.Application.Entensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMessageBrokerService, MessageBrokerService>();

            return services;
        }
    }
}
