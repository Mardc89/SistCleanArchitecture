using Application.Abstractions;
using Domain.Customers;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Create
{
    internal sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitWork;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository,IUnitOfWork unitOfWorks)
        {

            _customerRepository = customerRepository ?? throw new ArgumentNullException((nameof(customerRepository))) ;
            _unitWork = unitOfWorks ?? throw new ArgumentNullException(nameof(unitOfWorks));
            
        }
        public Task HandleAsync(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
