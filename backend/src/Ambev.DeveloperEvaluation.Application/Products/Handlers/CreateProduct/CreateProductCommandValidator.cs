using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Products.Handlers.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Dto.Title)
            .NotEmpty().WithMessage("O título do produto é obrigatório.")
            .MaximumLength(100).WithMessage("O título não pode exceder 100 caracteres.");

        RuleFor(x => x.Dto.Price)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

        RuleFor(x => x.Dto.Category)
            .NotEmpty().WithMessage("A categoria é obrigatória.")
            .MaximumLength(50).WithMessage("A categoria não pode exceder 50 caracteres.");

        RuleFor(x => x.Dto.Rating.Rate)
            .InclusiveBetween(0, 5).WithMessage("A avaliação deve estar entre 0 e 5.");

        RuleFor(x => x.Dto.Rating.Count)
            .GreaterThanOrEqualTo(0).WithMessage("O número de avaliações não pode ser negativo.");
    }
}