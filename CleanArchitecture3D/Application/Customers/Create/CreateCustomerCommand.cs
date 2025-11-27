using Application.Abstractions;

using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Create
{
    public record CreateCustomerCommand(
        string Name,
        string LastName,
        string Email,
        string PhoneNumber,
        string Country,
        string Linea1,
        string Linea2,
        string City,
        string State,
        string ZipCode):ICommand<ErrorOr<Unit>>;
    
}
