using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Create
{
    internal sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        public Task HandleAsync(CreateCustomerCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
