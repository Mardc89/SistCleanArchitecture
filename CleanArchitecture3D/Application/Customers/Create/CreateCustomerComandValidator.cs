using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Create
{
    public class CreateCustomerComandValidator:AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerComandValidator()
        {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(50);

            RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(50);
        }
    }
}
