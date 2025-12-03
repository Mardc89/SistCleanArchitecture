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

            RuleFor(x => x.LastName)
                    .NotEmpty()
                    .MaximumLength(50)
                    .WithName("Last Name");

            RuleFor(x => x.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .MaximumLength(255);

            RuleFor(x => x.PhoneNumber)
                    .NotEmpty()
                    .MaximumLength(9)
                    .WithName("PhoneNumber");

            RuleFor(x => x.Country)
                    .NotEmpty()
                    .MaximumLength(3);

            RuleFor(x => x.Linea1)
                    .NotEmpty()
                    .MaximumLength(20)
                    .WithName("Linea 1");

            RuleFor(x => x.Linea2)
                    .NotEmpty()
                    .MaximumLength(20)
                    .WithName("Linea 2");

            RuleFor(x => x.City)
                    .NotEmpty()
                    .MaximumLength(40);

            RuleFor(x => x.State)
                    .NotEmpty()
                    .MaximumLength(40);

            RuleFor(x => x.ZipCode)
                    .NotEmpty()
                    .MaximumLength(10)
                    .WithName("Zip Code"); 
        }
    }
}
