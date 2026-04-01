using FluentValidation;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Validators;

public class OrderStatusChangeValidator : AbstractValidator<OrderStatusChange>
{
    public OrderStatusChangeValidator()
    {
     

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required !")
            .Must(s => new [] {OrderStatus.Pending.ToString(),
                               OrderStatus.Shipped.ToString(),
                               OrderStatus.Delivered.ToString()}.Contains(s)).WithMessage("Status must be one of:0(Pending), 1(Shipped), 2(Delivered)");
    }
}
