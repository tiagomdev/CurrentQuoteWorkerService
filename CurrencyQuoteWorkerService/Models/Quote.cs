using System;
using System.Globalization;

namespace CurrencyQuoteWorker.Background.Models
{
    public class Quote
    {
        public Quote()
        {
        }

        public Quote(string recordLine)
        {
            var records = recordLine.Split(';');
            Value = decimal.Parse(records[0].Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            Code = int.Parse(records[1]);
            Date = DateTime.ParseExact(records[2], "d/M/yyyy", CultureInfo.InvariantCulture);
        }

        public decimal Value { get; set; }
        public int Code { get; set; }
        public DateTime Date { get; set; }
    }
}
