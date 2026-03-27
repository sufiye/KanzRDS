using FluentValidation;
using the_alkanz.Website.DTOs;


namespace Homework._08_ASP.NET_API.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage("Name is required")
                    .MinimumLength(3).WithMessage("Name must be at least 3 character long");

        RuleFor(x => x.LastName)
                  .NotEmpty().WithMessage("Name is required")
                  .MinimumLength(3).WithMessage("Name must be at least 3 character long");

        RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required")
                    .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required")
                    .MinimumLength(6)
                    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("invalid password");

        RuleFor(x => x.ConfirmedPassword)
                    .NotEmpty().WithMessage("ConfirmedPassword is required")
                    .Equal(x => x.Password).WithMessage("Passwords do not match");

    }
}
public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is required")
                 .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required")
                    .MinimumLength(6)
                    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("invalid password");
    }
}

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage("Name is required")
                    .MinimumLength(3).WithMessage("Name must be at least 3 character long");
        RuleFor(x => x.LastName)
                   .NotEmpty().WithMessage("Name is required")
                   .MinimumLength(3).WithMessage("Name must be at least 3 character long");
    }
}

public class PasswordUpdateValidator : AbstractValidator<PasswordUpdate>
{
    public PasswordUpdateValidator()
    {
        RuleFor(x => x.CurrentPassword)
                    .NotEmpty().WithMessage("CurrentPassword is required");

        RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required")
                    .MinimumLength(6)
                    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("invalid password");

        RuleFor(x => x.ConfirmedPassword)
                    .NotEmpty().WithMessage("ConfirmedPassword is required")
                    .MinimumLength(6)
                    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("invalid ConfirmedPassword")
                    .Equal(x => x.Password).WithMessage("Passwords do not match");


    }
}