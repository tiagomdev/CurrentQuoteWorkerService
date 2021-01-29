using System;

namespace CurrencyQuoteWorker.Background.Models
{
    public class CurrencyResult
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Value { get; set; }
    }
}
