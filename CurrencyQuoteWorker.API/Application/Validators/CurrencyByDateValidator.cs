using CurrencyQuoteWorkerService.Shared.DTOs;
using FluentValidation;

namespace CurrencyQuoteWorker.API.Application.Validators
{
    public class CurrencyByDateValidator : AbstractValidator<CurrencyByDateDTO>
    {
        public CurrencyByDateValidator()
        {
            RuleFor(c => c.Currency).NotEmpty().WithMessage("Id da moeda é obrigatorio.");
            RuleFor(m => m.StartDate)
                .NotEmpty()
                .WithMessage("Data inicial é obrigatorio.");

            RuleFor(m => m.EndDate)
                .NotEmpty().WithMessage("Data final é obrigatorio.")
                .GreaterThan(m => m.StartDate)
                .WithMessage("Data final deve ser maior que a data inicial.");
        }
    }
}
