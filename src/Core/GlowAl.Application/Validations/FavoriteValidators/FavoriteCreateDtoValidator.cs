using FluentValidation;
using GlowAl.Application.DTOs.FavoriteDtos;

namespace GlowAl.Application.Validators.FavoriteValidators;

    public class FavoriteCreateDtoValidator : AbstractValidator<FavoriteCreateDto>
    {
        public FavoriteCreateDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");
        }
    }


