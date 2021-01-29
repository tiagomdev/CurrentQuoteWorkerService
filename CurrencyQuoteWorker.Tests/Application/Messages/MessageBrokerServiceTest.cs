using CurrencyQuoteWorker.API.Excepions;
using CurrencyQuoteWorker.API.Application.Interfaces.Messages;
using CurrencyQuoteWorker.API.Application.Services.Messages;
using CurrencyQuoteWorkerService.Shared.DTOs;
using NUnit.Framework;
using System;
using CurrencyQuoteWorker.API.Infra.Interfaces.Services.Queue;
using Moq;

namespace CurrencyQuoteWorker.Tests.Application.Messages
{
    public class MessageBrokerServiceTest
    {
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void CatchAddFiltersToQueueWithCurrencyInvalid(string currency)
        {
            var filters = new CurrencyByDateDTO[]
                {
                    new CurrencyByDateDTO()
                    {
                        Currency = currency,
                        StartDate = new DateTime(2010, 1, 01),
                        EndDate = new DateTime(2010, 12, 01)
                    }
                };

            IMessageBrokerService messsageServiceMock = new MessageBrokerService(Mock.Of<IQueueService>());

            Assert.ThrowsAsync<DataModelValidationException>(async () => await messsageServiceMock.AddFiltersToQueue(filters));
        }

        [Test]
        public void CatchAddFiltersToQueueWithStartDateGreaterEndDate()
        {
            var filters = new CurrencyByDateDTO[]
                {
                    new CurrencyByDateDTO()
                    {
                        Currency = "USD",
                        StartDate = new DateTime(2010, 12, 01),
                        EndDate = new DateTime(2010, 6, 01)
                    }
                };

            IMessageBrokerService messsageServiceMock = new MessageBrokerService(Mock.Of<IQueueService>());

            Assert.ThrowsAsync<DataModelValidationException>(async () => await messsageServiceMock.AddFiltersToQueue(filters));
        }
    }
}
