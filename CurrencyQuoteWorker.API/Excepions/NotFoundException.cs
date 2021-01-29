using System;

namespace CurrencyQuoteWorker.API.Excepions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
