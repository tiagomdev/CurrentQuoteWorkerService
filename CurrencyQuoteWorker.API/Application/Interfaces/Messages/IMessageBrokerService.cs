using CurrencyQuoteWorkerService.Shared.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyQuoteWorker.API.Application.Interfaces.Messages
{
    public interface IMessageBrokerService
    {
        Task AddFiltersToQueue(CurrencyByDateDTO[] currencyByDateList, CancellationToken cancellationToken = default);

        CurrencyByDateDTO[] GetNextRecordsFromQueue();
    }
}
