using FluentValidation;
using GlowAl.Application.DTOs.CareProductDtos;

public class CareProductCreateValidator : AbstractValidator<CareProductCreateDto>
{
    public CareProductCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name boş ola bilməz.")
            .MaximumLength(150).WithMessage("Name maksimum 150 simvol ola bilər.");

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
            .GreaterThan(0).WithMessage("Price 0-dan böyük olmalıdır.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl boş ola bilməz.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId boş ola bilməz.");
    }
}



