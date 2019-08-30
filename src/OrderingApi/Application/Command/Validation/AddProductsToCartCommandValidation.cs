using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.Command.Validation
{
    public class AddProductsToCartCommandValidation : AbstractValidator<AddProductsToCartCommand> 
    {
        public AddProductsToCartCommandValidation()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("UserId is mandatory");

            RuleFor(x => x.ProductIds).NotNull().NotEmpty().WithMessage("ProductIds is mandatory");
        }
    }
}
