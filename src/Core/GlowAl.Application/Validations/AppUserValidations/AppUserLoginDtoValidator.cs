using FluentValidation;
using GlowAl.Application.DTOs.AppUserDtos;

namespace GlowAl.Application.Validations.UserValidations;

public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginDto>
{
    public AppUserLoginDtoValidator()
    {

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz.")
            .EmailAddress().WithMessage("Email düzgün formatda deyil.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə boş ola bilməz.")
            .MinimumLength(8).WithMessage("Şifrə ən az 8 simvol olmalıdır.");

    }
}
