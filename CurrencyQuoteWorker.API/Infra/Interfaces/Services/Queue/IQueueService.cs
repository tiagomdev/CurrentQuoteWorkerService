using CurrencyQuoteWorkerService.Shared.DTOs;

namespace CurrencyQuoteWorker.API.Infra.Interfaces.Services.Queue
{
    public interface IQueueService
    {
        void Publish(CurrencyByDateDTO[] records);

        CurrencyByDateDTO[] GetNextMessage();
    }
}
