using FluentValidation;
using GlowAl.Application.DTOs.CategoryDtos;

namespace GlowAl.Application.Validations.CategoryValidators;

public class CategoryGetDtoValidator : AbstractValidator<CategoryGetDto>
{
    public CategoryGetDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name con not be null.")
            .MinimumLength(3).WithMessage("Name should be minimum character.");

    }
}
