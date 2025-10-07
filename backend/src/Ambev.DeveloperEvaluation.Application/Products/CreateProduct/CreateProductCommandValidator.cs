using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validação do comando de criação de produto.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do produto não pode exceder 100 caracteres.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");


        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("A categoria é obrigatória.")
            .MaximumLength(50).WithMessage("A categoria não pode exceder 50 caracteres.");

    }
}
