using CurrencyQuoteWorker.API.Application.Validators;
using CurrencyQuoteWorker.API.Infra.Interfaces.Services.Queue;
using CurrencyQuoteWorkerService.Shared.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyQuoteWorker.API.Application.Services.Messages
{
    public class MessageBrokerService : BaseService, Interfaces.Messages.IMessageBrokerService
    {
        private readonly IQueueService _queueService;
        public MessageBrokerService(IQueueService queueService)
        {
            _queueService = queueService;
        }

        private async Task ValidateFilters(CurrencyByDateDTO[] currencyByDateList)
        {
            var validator = new CurrencyByDateValidator();
            foreach (var currencyByDate in currencyByDateList)
            {
                await ValidateEntity<CurrencyByDateDTO, CurrencyByDateValidator>(currencyByDate, validator);
            }
            
        }

        public async Task AddFiltersToQueue(CurrencyByDateDTO[] currencyByDateList, CancellationToken cancellationToken = default)
        {
            await ValidateFilters(currencyByDateList);

            _queueService.Publish(currencyByDateList);
        }

        public CurrencyByDateDTO[] GetNextRecordsFromQueue()
        {
            var result = _queueService.GetNextMessage();

            return result;
        }
    }
}
