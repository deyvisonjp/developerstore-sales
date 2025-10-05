using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Validator for DeleteSaleCommand that ensures a valid sale ID is provided
    /// </summary>
    public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
    {
        public DeleteSaleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID is required.");
        }
    }
}
