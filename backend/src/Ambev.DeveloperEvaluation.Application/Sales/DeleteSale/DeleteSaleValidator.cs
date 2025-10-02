using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Validator for <see cref="DeleteSaleCommand"/> that defines validation rules for deleting a sale.
/// </summary>
public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleCommandValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public DeleteSaleValidator()
    {
        RuleFor(sale => sale.Id)
            .NotEmpty().WithMessage("Sale ID is required.");
    }
}
