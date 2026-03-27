using FluentValidation;
using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Validators;

public class CategoryCreatRequestValidator : AbstractValidator<CreateCategoryRequestDto>
{
    public CategoryCreatRequestValidator()
    {
        RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name  is required")
                .MinimumLength(2).WithMessage("Category name must be at least 2 characters long");
    }
}
