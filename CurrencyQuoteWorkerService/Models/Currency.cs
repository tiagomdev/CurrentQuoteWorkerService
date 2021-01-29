using System;
using System.Globalization;

namespace CurrencyQuoteWorker.Background.Models
{
    public class Currency
    {
        public Currency()
        {
        }

        public Currency(string recordLine)
        {
            var records = recordLine.Split(';');
            Id = records[0];
            Date = DateTime.ParseExact(records[1], "yyyy-M-d", CultureInfo.InvariantCulture);
        }

        public string Id { get; set; }

        public DateTime Date { get; set; }
    }
}
