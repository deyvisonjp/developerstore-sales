using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for <see cref="UpdateSaleCommand"/> that defines validation rules for updating a sale.
/// </summary>
public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleCommandValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// - CustomerName: Required, at least 3 characters
    /// - SaleDate: Cannot be in the future
    /// - TotalAmount: Must be greater than zero
    /// </remarks>
    public UpdateSaleValidator()
    {
        RuleFor(sale => sale.Id)
            .NotEmpty().WithMessage("Sale ID is required.");

        RuleFor(sale => sale.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MinimumLength(3).WithMessage("Customer name must be at least 3 characters.");

        RuleFor(sale => sale.SaleDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.TotalAmount)
            .GreaterThan(0).WithMessage("Total amount must be greater than zero.");
    }
}
