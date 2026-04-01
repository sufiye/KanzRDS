using FluentValidation;
using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductRequestDto>
{
    public CreateProductValidator()
    {

        RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
              ;

        RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(5).WithMessage("Title must be at least 3 characters long")
                ;

        RuleFor(x => x.Description)
               .NotEmpty().WithMessage("Description is required")
               .MinimumLength(5).WithMessage("Description must be at least 3 characters long")
               ;

        RuleFor(x => x.CategoryId)
               .NotEmpty().WithMessage("Category is required");

        RuleFor(x => x.Price)
             .NotEmpty().WithMessage("Price is required")
             .GreaterThan(0).WithMessage("Price must be greater than zero");

        RuleFor(x => x. StockCount)
            .NotEmpty().WithMessage("StockCount is required")
            .GreaterThan(0).WithMessage("StockCount must be greater than zero");


    }
}

public class UpdateProductValidator : AbstractValidator<UpdateProductRequestDto>
{
    public UpdateProductValidator()
    {

        RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
                .MaximumLength(30);

        RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(5).WithMessage("Title must be at least 3 characters long")
                .MaximumLength(30);

        RuleFor(x => x.Description)
               .NotEmpty().WithMessage("Description is required")
               .MinimumLength(5).WithMessage("Description must be at least 3 characters long")
               .MaximumLength(30);

        RuleFor(x => x.CategoryId)
               .NotEmpty().WithMessage("Category is required");

        RuleFor(x => x.Price)
             .NotEmpty().WithMessage("Price is required")
             .GreaterThan(0).WithMessage("Price must be greater than zero");

        RuleFor(x => x.StockCount)
            .NotEmpty().WithMessage("StockCount is required")
            .GreaterThan(0).WithMessage("StockCount must be greater than zero");


    }
}