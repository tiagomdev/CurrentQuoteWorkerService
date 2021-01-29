using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyQuoteWorker.API.Excepions
{
    public class DataModelValidationException : Exception
    {
        public DataModelValidationException()
        {
        }

        public DataModelValidationException(string message) : base(message)
        {

        }

        public DataModelValidationException(ValidationResult validationResult) : base(GetErrorMessages(validationResult.Errors))
        {
        }

        static string GetErrorMessages(IList<ValidationFailure> errors)
        {
            var errorMessage = new StringBuilder();

            foreach (var item in errors)
                errorMessage.AppendLine(item.ErrorMessage);

            return errorMessage.ToString();
        }
    }
}
