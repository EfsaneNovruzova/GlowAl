using FluentValidation;
using GlowAl.Application.DTOs.CareProductDtos;

public class CareProductUpdateValidator : AbstractValidator<CareProductUpdateDto>
{
    public CareProductUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name boş ola bilməz.")
            .MaximumLength(150);

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Brand boş ola bilməz.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Ingredients)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ImageUrl)
            .NotEmpty();

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}

