using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Validator for GetSaleCommand that ensures a valid sale ID is provided
    /// </summary>
    public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
    {
        public GetSaleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID is required.");
        }
    }
}
