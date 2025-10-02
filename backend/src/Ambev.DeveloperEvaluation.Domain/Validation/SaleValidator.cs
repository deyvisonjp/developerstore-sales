
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;
public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.Customer)
            .NotEmpty().WithMessage("CustomerId is required.");

        RuleFor(sale => sale.Date)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("A sale must have at least one item.");
    }
}
