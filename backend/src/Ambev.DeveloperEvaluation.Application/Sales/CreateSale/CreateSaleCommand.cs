using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
/// <summary>
/// Command for creating a new sale.
/// </summary>
/// <remarks>
/// Captures the required data for creating a sale, including customer, products, amount, and date.
/// Implements <see cref="IRequest{TResponse}"/> to initiate the request that returns a <see cref="CreateSaleResult"/>.
/// Validation is handled via <see cref="CreateSaleCommandValidator"/>.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public string CustomerName { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Validates the current command using FluentValidation.
    /// </summary>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleCommandValidator();
        var result = validator.Validate(this);

        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}