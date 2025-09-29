using FluentValidation;
using GlowAl.Application.DTOs.ReviewDtos;

namespace GlowAl.Application.Validations.ReviewValidators;

public class ReviewCreateDtoValidator : AbstractValidator<ReviewCreateDto>
{
    public ReviewCreateDtoValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating 1 ilə 5 arasında olmalıdır.");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment boş ola bilməz.")
            .MaximumLength(500);
    }
}
