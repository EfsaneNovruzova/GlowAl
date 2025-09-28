using FluentValidation;

public class CareProductGetValidator : AbstractValidator<CareProductGetDto>
{
    public CareProductGetValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id boş ola bilməz.")
            .NotEqual(Guid.Empty).WithMessage("Id boş GUID ola bilməz.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name boş ola bilməz.")
            .MaximumLength(150);

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Brand boş ola bilməz.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description boş ola bilməz.")
            .MaximumLength(1000);

        RuleFor(x => x.Ingredients)
            .NotEmpty().WithMessage("Ingredients boş ola bilməz.")
            .MaximumLength(500);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price 0-dan böyük olmalıdır.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock 0-dan kiçik ola bilməz.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl boş ola bilməz.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId boş ola bilməz.");
    }
}

