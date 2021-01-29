using CurrencyQuoteWorker.API.Excepions;
using FluentValidation;
using System.Threading.Tasks;

namespace CurrencyQuoteWorker.API.Application.Services
{
    public abstract class BaseService
    {
        protected async Task ValidateEntity<T1, T2>(T1 updateEntity, AbstractValidator<T1> validator)
        {
            var validation = await validator.ValidateAsync(updateEntity);
            if (!validation.IsValid)
                throw new DataModelValidationException(validation);
        }
    }
}
