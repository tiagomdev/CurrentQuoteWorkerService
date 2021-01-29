using CurrencyQuoteWorker.API.Application.Interfaces.Messages;
using CurrencyQuoteWorkerService.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CurrencyQuoteWorker.API.Controllers
{
    [ApiController]
    [Route("api/queue")]
    public class QueueController : ControllerBase
    {
        private readonly IMessageBrokerService _messageService;

        public QueueController(IMessageBrokerService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [Route("next")]
        public IActionResult GetNextItem()
        {
            var result = _messageService.GetNextRecordsFromQueue();
            return Ok(result);
        }

        [HttpPost]
        [Route("publish")]
        public async Task<IActionResult> PublishFilters([FromBody] CurrencyByDateDTO[] filters)
        {
            await _messageService.AddFiltersToQueue(filters);
            return Ok();
        }
    }
}
