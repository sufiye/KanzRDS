using FluentValidation;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Validators;

public class CreateBasketItemRequestValidator : AbstractValidator<CreateBasketItemRequestDto>
{
    public CreateBasketItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required !");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Quantity is required !")
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");
    }
}
