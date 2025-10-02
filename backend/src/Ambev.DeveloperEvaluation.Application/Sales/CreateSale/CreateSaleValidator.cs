using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleCommand"/> that defines validation rules for sale creation command.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleCommandValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CustomerName: Required, must have at least 3 characters
    /// - SaleDate: Cannot be in the future
    /// - TotalAmount: Must be greater than zero
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MinimumLength(3).WithMessage("Customer name must be at least 3 characters.");

        RuleFor(sale => sale.SaleDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.TotalAmount)
            .GreaterThan(0).WithMessage("Total amount must be greater than zero.");
    }
}
